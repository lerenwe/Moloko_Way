using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour
{

	#region Members

        // References
				            private			Spaceship		    m_LinkedShip;
                            private         QteUI               m_LinkedQteUI;

	#endregion


	#region Initialisation

		void Start()
		{
			m_LinkedShip = GetComponent<Spaceship>();
            m_LinkedQteUI = QteManager.GetQteUI();
		}

	#endregion


	#region Updating (Check controls)

		void Update()
        {
            if(!GameManager.IsGameOver() && !m_LinkedShip.IsRunningHitStateTimer())
            {
                CheckArrowsControls();
				CheckMouseControls();
			}
		}

	#endregion


	#region Reactions

		void CheckArrowsControls()
		{
			// No key controls
		}

		void CheckMouseControls()
		{
            // Throwing spaceman
			if(m_LinkedShip.IsSpacemanInShip() && !m_LinkedShip.IsLanded() && !m_LinkedShip.IsShipTooClose())
			{
				if(Input.GetMouseButton(0))
					m_LinkedShip.AddThrowForce();

				if(Input.GetMouseButtonUp(0))
					m_LinkedShip.ThrowSpaceman();
			}

            else if (!m_LinkedShip.IsSpacemanInShip() && m_LinkedShip.IsLanded() && m_LinkedShip.IsLoadedResources())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (m_LinkedShip.GetRequiredControl() == 0)
                    {
                        ScoreManager.AddScore(m_LinkedShip.GetScorePerResource());
                    }

                    m_LinkedShip.UnloadResource();
                    m_LinkedShip.ResetQteTimer();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (m_LinkedShip.GetRequiredControl() == 1)
                    {
                        ScoreManager.AddScore(m_LinkedShip.GetScorePerResource());
                    }

                    m_LinkedShip.UnloadResource();
                    m_LinkedShip.ResetQteTimer();
                }
            }

			// Landing moves
			float mouseScrollWheelAxis = Input.GetAxis ("Mouse ScrollWheel");

			if(mouseScrollWheelAxis > 0)
				m_LinkedShip.Move(Vector3.up);
			else if(mouseScrollWheelAxis < 0)
				m_LinkedShip.Move (Vector3.down);
		}

	#endregion

}
