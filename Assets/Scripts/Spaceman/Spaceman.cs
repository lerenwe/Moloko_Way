using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spaceman : MonoBehaviour
{
    [SerializeField]int m_NumberSlot=3;
    [SerializeField]float m_FolowingSpeed=10;
    [SerializeField]float m_FolowingDifferenceFactor=1;
    [SerializeField]float m_ResourceDistanceToPlayer = 5;
    [SerializeField]float m_DistanceToEnterSpaceShip = 1;
	List<GameObject> m_Resources;
	public float m_Oxygen = 100;
	public float m_OxygenMax = 100;
    Collision m_Col = null;
    GameSettings m_Game;
	Animator m_anim;
	Animator m_animUI;
	Animator m_animUIVisage;
	bool isDead = false;
	float m_DeltaTime=0;

    void Awake()
    {
        PlayersManager.m_Spaceman = gameObject;
        
    }

    void Start()
    {
        m_Game = GameManager.GetGameSettings();
        gameObject.SetActive(false);
        transform.position = PlayersManager.m_Spaceship.transform.position;
        m_Resources = new List<GameObject>();
		m_Oxygen = m_OxygenMax;
		m_anim = GetComponent <Animator> ();
		m_animUI = GameObject.FindObjectOfType<OxygenUIManager>().m_anim;
		m_animUIVisage = GameObject.FindObjectOfType<OxygenVisageUIManager>().m_anim;
    }

    /// <summary>
    /// If the spaceman is too far from the planet, it's game over
    /// </summary>
    void Update()
    {
		 if (!isDead) {
						if (Vector3.Distance (transform.position, PlanetManager.GetPlanet ().transform.position) >= m_Game.GetDistanceToGameOver ()) {
								m_anim.SetTrigger ("IsDead");
								isDead = true;
						}


						if (Vector3.Distance (transform.position, PlanetManager.GetPlanet ().transform.position) <= m_Game.GetDistanceToCrash ()) {
								
								m_anim.SetTrigger ("IsFire");
								isDead = true;
						}

						if (m_Oxygen <= 0) {
								
								m_anim.SetTrigger ("IsDead");
								isDead = true;
						}


						if (Vector3.Distance (transform.position, PlayersManager.m_Spaceship.transform.position) <= m_DistanceToEnterSpaceShip) {
								EnterSpaceship ();
						} else {

								int i = 0;
								foreach (GameObject go in m_Resources) {
                
										go.GetComponent<PickUp> ().OnPicked ();
										go.transform.position = Vector3.Lerp (go.transform.position, transform.position + (transform.right * (m_ResourceDistanceToPlayer + i * m_FolowingDifferenceFactor)), Time.deltaTime * m_FolowingSpeed);
										i++;
										//Debug.Break();
								}
						}
				} else {
			m_DeltaTime+=Time.deltaTime;
			if(m_DeltaTime>4)
			{
				GameManager.SetGameOver ();
				
			}
					
				}
    }


    /// <summary>
    /// If the cosmonaute collide with the planet, it's game over
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Resource")
        {
            Debug.Log("Resource !!");
            AddResource(col.gameObject);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        m_Col = col;
        Orbitation colliderOrbitation = col.collider.GetComponent<Orbitation>();
        Orbitation spacemanOrbitaton = GetComponent<Orbitation>();

		m_anim.SetTrigger ("Collide");

        if (colliderOrbitation != null && spacemanOrbitaton != null)
        {
            float speedCollidedObject = colliderOrbitation.m_OrbitationSpeed;
            float speedSpaceman = spacemanOrbitaton.m_OrbitationSpeed;

            //bounce on the collider

            if (speedSpaceman > speedCollidedObject)//the spaceman come from behind the asteroid. bounce backward
            {
                colliderOrbitation.m_OrbitationSpeed = speedSpaceman;
                spacemanOrbitaton.m_OrbitationSpeed = speedCollidedObject;
            }
            else//bounce forward
            {
                spacemanOrbitaton.m_OrbitationSpeed = speedCollidedObject;
                colliderOrbitation.m_OrbitationSpeed = speedSpaceman;
            }

            
        }
    }


    public void LoadResources(List<GameObject> resources)
    {
        m_Resources.Clear();
        int i = 0;
        foreach (GameObject r in resources)
        {
            AddResource(r,i);
            i = 0;
        }
    }

    public void AddResource(GameObject resource,int i=0)
    {
        if (m_Resources.Count < m_NumberSlot)
        {
            Debug.Log("Tac !!");
            resource.GetComponent<Orbitation>().enabled = false;
            resource.GetComponent<PickUp>().OnGetPickUp();
            resource.collider.enabled = false;
            resource.transform.position = transform.position +(transform.right * (m_ResourceDistanceToPlayer+i*m_FolowingDifferenceFactor));
			resource.transform.right=transform.right;
            m_Resources.Add(resource);
        }
    }

    void EnterSpaceship()
    {
        foreach (GameObject go in m_Resources)
        {
            go.renderer.enabled = false;
            go.collider.enabled = false;
        }
        PlayersManager.m_Spaceship.GetComponent<Spaceship>().LoadResources(m_Resources);
    }

    public Orbitation GetOrbitation()
    {
        return GetComponent<Orbitation>();
    }

    public void OnDrawGizmos()
    {
        if (m_Col != null)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(m_Col.transform.position, transform.position);
        }
    }

	public void ResetOxygen()
	{
		m_Oxygen = m_OxygenMax;
		m_animUI.SetTrigger ("OxygenUp");
		m_animUIVisage.SetTrigger ("OxygenUp");
	}

}