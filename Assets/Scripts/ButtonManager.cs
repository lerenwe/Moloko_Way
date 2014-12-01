using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {

	#region Members
	
		private		Animator		m_Anim;
		private		int 			m_NumButton		= 1;
		private		bool 			m_IsOnCredits 	= false;
	
	#endregion


	#region Initialisation
	
		void Start ()
		{
			// Get the animator on object
			m_Anim = GetComponent <Animator> ();
		}
		
	#endregion
	
	
	#region Updating
	
		void Update ()
		{
			// Arrows are used to select a menu
			CheckArrowsControls();
			
			// Keys are used to open the selected menu
			CheckKeysControls();
		}
		
	#endregion
	
	
	#region Controllers
	
		private void CheckArrowsControls()
		{
			// GetKey() 	return true while the named key is pressed
			// GetKeyDown() return true only one time, at the first frame where the named key is pressed
			if (Input.GetKeyDown (KeyCode.DownArrow))
			{
				m_NumButton++;
				m_Anim.SetTrigger("Down");
			}
			
			else if (Input.GetKeyDown (KeyCode.UpArrow))
			{
				m_NumButton--;
				m_Anim.SetTrigger("Up");
			}
	
			// Correction of chose button
			
			if (m_NumButton < 1) 
			{
				m_NumButton = 4;
			} 
			
			else if (m_NumButton > 4) 
			{
				m_NumButton=1;
			}
		}
		
		private void CheckKeysControls()
		{
			// If user is on credits page, the Escape key is enabled. If that key is pressed...
			if(m_IsOnCredits && Input.GetKeyDown (KeyCode.Escape))
			{
				// ... User exit the page
				m_Anim.SetTrigger("Echap");
				m_IsOnCredits = false;
			}
	
			// If Enter key is pressed
			else if(Input.GetKeyDown(KeyCode.Return))
			{
				switch(m_NumButton)
				{
					 case 1: 										// If the first menu is selected ("Play")
					 	Application.LoadLevel("Scene_01");			// Load the first level
						break;
						
					 case 3:										// If the third menu is selected ("Credits")
					 	m_IsOnCredits = true;						// Enable the Escape key to exit the current page
					 	m_Anim.SetTrigger("Clic");					// ... Qu'est-ce que dafuck ?
						break;
				}
			}
		}
	
	#endregion
	
}
