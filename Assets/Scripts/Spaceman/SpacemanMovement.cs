using UnityEngine;
using System.Collections;

public class SpacemanMovement : MonoBehaviour
{
	#region Members
	
	[SerializeField]public KeyCode m_UpButton=KeyCode.Z;
	[SerializeField]public KeyCode m_DownButton=KeyCode.S;
	[SerializeField]float   m_MovementForce;
	Vector3 m_OrbitationCenter;
	Spaceman spaceman;
	Animator m_anim;
	
	#endregion
	
	#region Monobehaviour Manipulators
	
	// Use this for initialization
	void Start()
	{
		m_OrbitationCenter=PlayersManager.m_Spaceman.GetComponent<Orbitation>().m_OrbitationCenter;
		spaceman = GetComponent <Spaceman> ();
		m_anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector3 positionToRotationCenter=transform.position-m_OrbitationCenter;
		positionToRotationCenter.Normalize();
		if (Input.GetKey(m_UpButton))//Move the player further to the planet
		{
			rigidbody.AddForce(positionToRotationCenter*m_MovementForce);
			spaceman.m_Oxygen -= 0.1f;
			m_anim.SetTrigger("Up");
			
		}
		else if (Input.GetKey(m_DownButton)) //Move the player closer to the planet
		{
			rigidbody.AddForce(-positionToRotationCenter*m_MovementForce);
			spaceman.m_Oxygen -= 0.1f; 
			m_anim.SetTrigger("Down");
		}
	}
	#endregion
}
