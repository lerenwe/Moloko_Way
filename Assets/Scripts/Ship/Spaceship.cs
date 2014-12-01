using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spaceship : MonoBehaviour
{

	#region Members

		// References
							private			GameObject			m_Spaceman;
                            private         Planet              m_Planet;

        // Physics
        [SerializeField]    private         float               m_DefaultReboundLevel           = 200000.0f;
                            private         Vector3             m_ActualPosition;
		[SerializeField]	private			string[]			m_ObstaclesTags;
                            private         float               m_ActualVelocity;

        // Fuel
        [SerializeField]    private         float               m_FuelMax                       = 100.0f;
        [SerializeField]    private         float               m_FuelHealLevel                 = 0.08f;
        [SerializeField]    private         float               m_FuelLoseLevel                 = 0.07f;
                            private         float               m_Fuel;

		// Landing
		[SerializeField]	private			float				m_AddForceLevel                 = 1200.0f;
							private			float				m_CurrentSpeed;
							private			bool				m_IsLanded                      = true;

		// Hit management
		[SerializeField]	private			float				m_HitStateTime                  = 2.0f;
							private			float				m_HitStateTimer;
        [SerializeField]    private         float               m_PercentageResourcesToLose     = 30.0f;
        [SerializeField]    private         float               m_InvincibilityStateTime        = 1.0f;
                            private         float               m_InvincibilityStateTimer       = 0;
																
		// Limiters										 	
		[SerializeField]	private			float				m_SpeedLimitToCrash             = 2.0f;
																
		// Resources											
		[SerializeField]	private			int					m_ResourcesMax                  = 6;
							private			List<GameObject>	m_LoadedResources			    = new List<GameObject>();
                            private         List<GameObject>    m_ResourcesToSpaceman           = new List<GameObject>();

		// Spaceman and throwing
							private			bool				m_IsSpacemanInShip			    = true;
		[SerializeField]	private			float				m_AddThrowForceLevel            = 12.0f;
		[SerializeField]	private			float				m_MinThrowForceLevel            = 10.0f;
							private			float				m_ThrowForce;
		[SerializeField]	private			float				m_SpacemanReceptionOffset       = 0.9f;

        // QTE
                            private         float               m_QteTimer;
        [SerializeField]    private         float               m_QteMaxTimeToClick             = 1.0f;
                            private         int                 m_RequiredControl;
                            private         QteUI               m_LinkedQteUI;

        // Scoring
        [SerializeField]    private         int                 m_ScorePerResource              = 100;
		
							private   Animator m_Anim;

	#endregion


	#region Initialisation

		void Awake()
		{
			PlayersManager.m_Spaceship = gameObject;
		}

		void Start()
		{
			m_ThrowForce                = m_MinThrowForceLevel;
            m_HitStateTimer             = m_HitStateTime;
            m_InvincibilityStateTime    += m_HitStateTime;
            m_Spaceman                  = PlayersManager.m_Spaceman;
            m_Fuel                      = m_FuelMax;

            m_Planet = PlanetManager.GetPlanet();
            transform.position = m_Planet.transform.position + (Vector3.up * m_Planet.collider.bounds.size.y)/2;

            m_LinkedQteUI = QteManager.GetQteUI();
			m_Anim = GameObject.FindObjectOfType<VaisseauUIManager> ().m_anim;
		}

	#endregion


	#region Updating

		void Update()
        {
			if(IsSpaceshipTooFar())
                GameManager.SetGameOver();

            if (IsFuelDown())
                GameManager.SetGameOver();

            if (!GameManager.IsGameOver())
            {
                if (IsRunningHitStateTimer())
                    UpdateHitStateTimer();
                else
                    m_ActualPosition = new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z);

                m_ActualVelocity = rigidbody.velocity.y;

                UpdateFuel();
                UpdateInvincibilityTimer();

                if (!IsSpacemanInShip() && IsLanded() && IsLoadedResources())
                    UpdateQteTimer();
            }
		}

	#endregion


	#region Actions

		void OnCollisionEnter(Collision collision)
		{
			Planet planet = collision.gameObject.GetComponent<Planet>();

			if(planet)
            {
                if(m_ActualVelocity < 0 && m_ActualVelocity < -m_SpeedLimitToCrash)
					GameManager.SetGameOver();												// Ship crashed
				else
                    //if (!m_IsSpacemanInShip)
                        Land();
			}
            
			foreach(string tag in m_ObstaclesTags)
            {
                if (collision.collider.tag == tag && !IsRunningInvincibilityTimer())
                {
                    Vector3 spaceshipToCollider = collision.gameObject.transform.position - transform.position;
                    float angle = Vector3.Dot(spaceshipToCollider.normalized, Vector3.right);

                    Vector3 reboundDirection = Vector3.zero;

                    if (angle >= 0 && angle < 0.5)
                        reboundDirection = (Vector3.right + Vector3.down).normalized;
                    else if (angle >= 0.5 && angle <= 1)
                        reboundDirection = (Vector3.up + Vector3.right).normalized;
                    else if (angle < 0 && angle >= -0.5)
                        reboundDirection = (Vector3.down + Vector3.left).normalized;
                    else
                        reboundDirection = (Vector3.left + Vector3.up).normalized;
                    
                    collision.rigidbody.AddForce(reboundDirection * m_DefaultReboundLevel);

                    // Up 0.5 = Up + right
                    // Down 0.5 = Right + down
                    // 0 to - 0.5 = Down + left
                    // -0.5 to -1 = Left + up

                    OnHit();
                }
			}
		}

		void OnCollisionExit(Collision collision)
		{
			m_IsLanded = false;
            m_LinkedQteUI.ResetAnimators();
		}

        private void Land()
        {
            m_IsLanded = true;
            ResetQteTimer();
            // Animation of landing
        }

		public void Move(Vector3 direction)
        {
            rigidbody.AddForce(direction * m_AddForceLevel);
		}

		public void ThrowSpaceman()
        {
            m_Spaceman.transform.position = transform.position + -Vector3.right * m_SpacemanReceptionOffset;

            m_Spaceman.GetComponent<Spaceman>().LoadResources(m_ResourcesToSpaceman);

			m_Spaceman.gameObject.SetActive(true);

			m_Spaceman.rigidbody.AddForce(-Vector3.right * (m_ThrowForce/10));

			m_ThrowForce 		= m_MinThrowForceLevel;
			m_IsSpacemanInShip 	= false;
		}

		void OnHit()
		{
			m_HitStateTimer = 0;
            m_InvincibilityStateTimer = 0;

            rigidbody.velocity = Vector3.zero;

            float resourcesToLose = Mathf.Ceil((float)m_LoadedResources.Count * m_PercentageResourcesToLose / 100.0f);

            for (int i = 0; i < resourcesToLose; i++)
            {
                m_LoadedResources.Remove(m_LoadedResources[m_LoadedResources.Count - 1]);
            }
        }

		public void LoadResources(List<GameObject> resources)
		{
            if (!IsLanded())
            {
                m_Spaceman.SetActive(false);
                m_Spaceman.transform.position = transform.position;
                m_Spaceman.GetComponent<Spaceman>().ResetOxygen();
				

                m_IsSpacemanInShip = true;

                int slotsToLoad = GetFreeSlots();

                if (resources.Count < slotsToLoad)
                    slotsToLoad = resources.Count;

                for (int i = 0; i < slotsToLoad; i++)
                {
                    m_LoadedResources.Add(resources[resources.Count - 1]);
                    resources.Remove(resources[resources.Count - 1]);
                }
            }

            m_ResourcesToSpaceman = resources;
		}

        public void UnloadResource()
        {
			if(!IsSpacemanInShip())
			{
	            GameObject unloadedResource = m_LoadedResources[m_LoadedResources.Count - 1];
	            m_LoadedResources.Remove(unloadedResource);
			 }
        }

    #endregion


    #region Updaters

        void UpdateHitStateTimer()
		{
            if (IsRunningHitStateTimer())
            {
                m_HitStateTimer += Time.deltaTime;
                transform.position = m_ActualPosition;
            }
		}

        void UpdateQteTimer()
        {
            if (m_RequiredControl == 0)
                m_LinkedQteUI.SetLeftClickAnimator(true);
            else if (m_RequiredControl == 1)
                m_LinkedQteUI.SetRightClickAnimator(true);
            
            if (m_QteTimer < m_QteMaxTimeToClick)
                m_QteTimer += Time.deltaTime;
            else
            {
                UnloadResource();
                ResetQteTimer();
            }
        }

        void UpdateFuel()
        {
            if (m_IsLanded)
            {
                m_Fuel += m_FuelHealLevel;
                if (m_Fuel > m_FuelMax)
                    m_Fuel = m_FuelMax;
					m_Anim.SetTrigger("FuelUp");
            }
            else
            {
                m_Fuel -= m_FuelLoseLevel;
                if (m_Fuel < 0)
                    GameManager.SetGameOver();
            }
        }

        void UpdateInvincibilityTimer()
        {
            if (IsRunningInvincibilityTimer())
                m_InvincibilityStateTimer += Time.deltaTime;
        }

	#endregion


	#region Checkers

		public bool IsSpaceshipTooFar()
		{
			return (GetDistanceToPlanet() > GameManager.GetGameSettings().GetDistanceToGameOver());
		}

		public bool IsSpacemanInShip()
		{
			return m_IsSpacemanInShip;
		}

		public bool IsSpacemanCloseEnough()
		{
			return (Vector3.Distance(transform.position, m_Spaceman.transform.position) < m_SpacemanReceptionOffset);
		}

		public bool IsRunningHitStateTimer()
        {
            return (m_HitStateTimer < m_HitStateTime);
		}

        public bool IsLanded()
        {
            return m_IsLanded;
        }

        public bool IsFuelDown()
        {
            return (m_Fuel <= 0);
        }

        public bool IsRunningInvincibilityTimer()
        {
            return (m_InvincibilityStateTimer < m_InvincibilityStateTime);
        }

        public bool IsLoadedResources()
        {
            return (m_LoadedResources.Count > 0);
        }

        public bool IsShipTooClose()
        {
            return (Vector3.Distance(m_Planet.transform.position, transform.position) <= GameManager.GetGameSettings().GetMinOrbitation());
        }

	#endregion


	#region Getters

		public float GetDistanceToPlanet()
		{
			return Vector3.Distance(transform.position, PlanetManager.GetPlanet().transform.position);
		}

		public float GetThrowForce()
		{
			return m_ThrowForce;
		}

		public int GetFreeSlots()
		{
			return (m_ResourcesMax - m_LoadedResources.Count);
		}

		public int GetTotalSlots()
		{
			return m_ResourcesMax;
		}

        public float GetFuel()
        {
            return m_Fuel;
        }

        public float GetFuelMax()
        {
            return m_FuelMax;
        }

        public List<GameObject> GetResources()
        {
            return m_LoadedResources;
        }

        public int GetRequiredControl()
        {
            return m_RequiredControl;
        }

        public int GetScorePerResource()
        {
            return m_ScorePerResource;
        }

	#endregion


	#region Setters

		public void SetSpacemanInShip(bool isInside)
		{
			m_IsSpacemanInShip = isInside;
		}

		public void AddThrowForce()
		{
			m_ThrowForce += m_AddThrowForceLevel;
		}

		public void AddSpeed(float speed)
		{
			m_CurrentSpeed += speed;
		}

        public void ResetQteTimer()
        {
            m_QteTimer = 0;
            m_RequiredControl = Random.Range(0, 2);
            m_LinkedQteUI.ResetAnimators();
        }

	#endregion

}
