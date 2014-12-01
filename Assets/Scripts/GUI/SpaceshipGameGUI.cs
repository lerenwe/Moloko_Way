using UnityEngine;
using System.Collections;

public class SpaceshipGameGUI : MonoBehaviour
{

	#region Members

		                        private     Spaceship   m_LinkedSpaceship;
        [SerializeField]        private     bool        m_ActiveGUIDebug            = false;

	#endregion


	#region Initialisation

		void Start()
		{
			m_LinkedSpaceship = GetComponent<Spaceship>();
		}

	#endregion


	#region GUI

		void OnGUI()
		{
            if (m_ActiveGUIDebug)
            {
                if (GameManager.IsGameOver())
                    GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12, 200, 24), "Game over !");

                else
                {
                    GUI.Box(new Rect(5, 5, 300, 130), "");
                    GUI.Label(new Rect(10, 10, 290, 24), "Power : " + m_LinkedSpaceship.GetThrowForce());
                    GUI.Label(new Rect(10, 30, 290, 24), "Fuel : " + Mathf.Floor(m_LinkedSpaceship.GetFuel()) + " / " + m_LinkedSpaceship.GetFuelMax());

                    if (m_LinkedSpaceship.IsRunningHitStateTimer())
                        GUI.Label(new Rect(10, 50, 290, 24), "Hit !");
                    if(m_LinkedSpaceship.IsLanded())
                        GUI.Label(new Rect(10, 70, 290, 24), "Landed !");
                    if(m_LinkedSpaceship.IsSpacemanInShip())
                        GUI.Label(new Rect(10, 90, 290, 24), "Spaceman is in !");
                    if (m_LinkedSpaceship.IsRunningInvincibilityTimer())
                        GUI.Label(new Rect(10, 110, 290, 24), "Invincible !");
                }

                /*if (m_LinkedSpaceship.IsLanded() && m_LinkedSpaceship.IsLoadedResources() && !m_LinkedSpaceship.IsSpacemanInShip())
                {
                    float widthCenter = Screen.width / 2;
                    float heightCenter = Screen.height / 2;

                    GUI.Box(new Rect(widthCenter - 50, heightCenter - 100, 100, 200), "");
                    GUI.Box(new Rect(widthCenter - 45, heightCenter - 95, 90, 24), "QTE");
                    GUI.Label(new Rect(widthCenter - 35, heightCenter - 68, 42, 24), "Left");
                    GUI.Label(new Rect(widthCenter + 8, heightCenter - 68, 43, 24), "Right");

                    GUI.Box(new Rect(widthCenter - 45, heightCenter - 40, 42, 135), "");
                    GUI.Box(new Rect(widthCenter + 1, heightCenter - 40, 43, 135), "");

                    GUI.Box(new Rect(widthCenter - 55, heightCenter + 58, 110, 10), "");

                    GUI.Box(new Rect(widthCenter - 35, heightCenter + 70, 22, 22), "");
                    GUI.Box(new Rect(widthCenter + 11, heightCenter + 70, 22, 22), "");
                }*/
            }
		}

	#endregion


    #region Actions

        public void LaunchQTE()
        {
            //m_
        }

    #endregion

}
