using UnityEngine;
using System.Collections;

public class VaisseauUIManager : MonoBehaviour {

	GameObject m_player;
	Spaceship m_spaceship;
	public Animator m_anim;
	float m_PreviousFuelLevel;
	bool m_VaisseauFound = false;


	// Use this for initialization
	void Start () {

		m_anim = GetComponent <Animator> ();
		m_spaceship = PlayersManager.m_Spaceship.GetComponent <Spaceship> ();
		
		m_PreviousFuelLevel = m_spaceship.GetFuelMax();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		float FuelLost;



		FuelLost = m_PreviousFuelLevel - m_spaceship.GetFuel();


		float percentageLost=(FuelLost*100)/m_PreviousFuelLevel;


			if (percentageLost >= 10){

				m_anim.SetTrigger("FuelLost");
			m_PreviousFuelLevel = m_spaceship.GetFuel();
			}
		}
	}

