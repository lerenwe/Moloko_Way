using UnityEngine;
using System.Collections;

public class SpaceshipGameGUI : MonoBehaviour
{

	#region Members

		// References
		                        private     Spaceship   m_LinkedSpaceship;
		                        
		// Activators
        [SerializeField]        private     bool        m_ActiveGUIDebug            = false;
        [SerializeField]		private		bool		m_ActivateScoring			= true;

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
            if (GameManager.IsGameOver())
            	GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12, 200, 24), "Game over !");
            	
            if (m_ActiveGUIDebug && !GameManager.IsGameOver())
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
            
            if(m_ActivateScoring)
            	GUI.Box (new Rect(5, 140, 300, 24), ScoreManager.GetScore() + "");
		}

	#endregion

}
