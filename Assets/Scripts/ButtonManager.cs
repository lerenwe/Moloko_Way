using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {

	Animator m_anim;
	int m_NumButton=1;
	bool onCredit = false;

	// Use this for initialization
	void Start () {

		m_anim = GetComponent <Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		//Debug.Log (m_anim.GetCurrentAnimationClipState);

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			m_NumButton++;
			m_anim.SetTrigger("Down");

		}
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			m_NumButton--;
			m_anim.SetTrigger("Up");

		}

		if (m_NumButton < 1) 
		{
						m_NumButton = 4;
				
		} 
		else if (m_NumButton > 4) 
		{
			m_NumButton=1;
		}

	
		if (onCredit && Input.GetKey (KeyCode.Escape) ) {

			m_anim.SetTrigger("Echap");
				onCredit = false;
		}

		else if(Input.GetKey(KeyCode.Return))
		{
			Debug.Log ("retiur m_NumButton="+m_NumButton);
			switch(m_NumButton)
			{
			 case 1: Application.LoadLevel("Scene_01");
				break;
			case 3: onCredit=true; m_anim.SetTrigger("Clic");
				break;
			}
		}
	}
}
