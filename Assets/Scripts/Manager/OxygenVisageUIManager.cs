using UnityEngine;
using System.Collections;

public class OxygenVisageUIManager : MonoBehaviour {
	
	GameObject m_player;
	Spaceman m_spaceman;
	public Animator m_anim;
	float m_PreviousOxygenLevel;
	
	// Use this for initialization
	void Start () {
		
		
		m_anim = GetComponent <Animator> ();
		m_spaceman = PlayersManager.m_Spaceman.GetComponent <Spaceman> ();
		
		m_PreviousOxygenLevel = m_spaceman.m_Oxygen;
	}
	
	// Update is called once per frame
	void Update () {
		

		
		float OxygenLost;
		
		OxygenLost = m_PreviousOxygenLevel - m_spaceman.m_Oxygen;
		
		
		
		float percentageLost=(OxygenLost*100)/m_PreviousOxygenLevel;
		
		if (percentageLost >= 25){
			m_anim.SetTrigger("OxygenLost");
			m_PreviousOxygenLevel = m_spaceman.m_Oxygen;
			
		}
}
}
