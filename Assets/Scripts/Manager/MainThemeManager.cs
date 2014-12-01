using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainThemeManager : MonoBehaviour {

	
	GameObject m_spaceman;
	Planet m_planete;
	public List<AudioSource> m_Sound;
	bool m_dangerplay = false;
	AudioSource dangerZone;
	float mytime = 0f;

	// Use this for initialization
	void Start () {
		m_spaceman = PlayersManager.m_Spaceman;
		
		m_planete = PlanetManager.GetPlanet();

		foreach (AudioSource a in GetComponents<AudioSource>())
		{
			m_Sound.Add(a);
		}

	}
	
	// Update is called once per frame
	void Update () {
	



		if (m_dangerplay == true) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("musique_danger")){
					mytime = mytime + Time.deltaTime;
					if(mytime >= 0.3){
						a.volume = a.volume + 0.1f;
						mytime = 0f;
					}
					Debug.Log(a.volume);
				}
			}

		}

		if (m_dangerplay == false) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("musique_danger")){
					mytime = mytime + Time.deltaTime;
					if(mytime >= 0.3){
						a.volume = a.volume - 0.1f;
						mytime = 0f;
					}
					Debug.Log(a.volume);
				}
			}
			
		}


		if (Vector3.Distance (m_spaceman.transform.position, m_planete.transform.position) > 7 && m_dangerplay == false) {
			foreach (AudioSource a in m_Sound) 
			{
				if(a.clip.ToString().ToLower().Contains("musique_danger")){
					
					a.Play();
				}
			}
			m_dangerplay = true;
		}

		if (Vector3.Distance (m_spaceman.transform.position, m_planete.transform.position) < 7 && m_dangerplay == true) {
			m_dangerplay = false;
		}

	}
}
