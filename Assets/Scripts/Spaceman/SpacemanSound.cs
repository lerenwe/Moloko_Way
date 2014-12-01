using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpacemanSound : MonoBehaviour {

	Planet m_planete;
	bool m_BreathFLag = false;
	bool m_PropulserFlagUp = false;
	bool m_PropulserFlagDown = false;
	List<AudioSource> m_Sound;
	SpacemanMovement m_mouvement;
	public AudioClip m_breath;
	Animator m_anim;

	// Use this for initialization
	void Start () {
	
		m_Sound = new List<AudioSource> ();

		m_planete = PlanetManager.GetPlanet();
		foreach (AudioSource a in GetComponents<AudioSource>())
		{
			m_Sound.Add(a);
		}
		m_mouvement = GetComponent <SpacemanMovement> ();

		m_anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Breathing ();
		Propulsion ();

	}

	void OnCollisionEnter (Collision other){

		switch(Random.Range (0, 4)){
			case 1 :
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_impact_asteroide_01"))
					a.Play();
			}
			break;
			case 2 :
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_impact_asteroide_02"))
					a.Play();
			}
			break;
			case 3 :
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_impact_asteroide_03"))
					a.Play();
			}
			break;
			case 4 :
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_impact_asteroide_04"))
					a.Play();
			}
			break;
		}
	}


	void Breathing (){

		if (Vector3.Distance (transform.position, m_planete.transform.position) > 7 && m_BreathFLag == false) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_breath"))
					a.Play();
			}
			m_BreathFLag = true;


		} else if (Vector3.Distance (transform.position, m_planete.transform.position) < 7 && m_BreathFLag == true) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_breath"))
					a.Stop();
			}
			m_BreathFLag = false;
		}
	}

	void Propulsion(){

		if (Input.GetKey (m_mouvement.m_UpButton) && m_PropulserFlagUp == false) {

			foreach (AudioSource a in m_Sound) 
			{

				if(a.clip.ToString().ToLower().Contains("spaceman_propulseurs")){
					a.Play();
					m_PropulserFlagUp = true;
				}
			}
		} 
		else if (!Input.GetKey (m_mouvement.m_UpButton) && m_PropulserFlagUp == true) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_propulseurs"))
					a.Stop();
					m_PropulserFlagUp = false;
			}
		}
		else if (Input.GetKey (m_mouvement.m_DownButton) && m_PropulserFlagDown == false) {
			
			foreach (AudioSource a in m_Sound) 
			{
				
				if(a.clip.ToString().ToLower().Contains("spaceman_propulseurs")){
					a.Play();
					m_PropulserFlagDown = true;
				}
			}
		}
		else if (!Input.GetKey (m_mouvement.m_DownButton) && m_PropulserFlagDown == true) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("spaceman_propulseurs"))
					a.Stop();
				m_PropulserFlagDown = false;
			}
		}
	}


}
