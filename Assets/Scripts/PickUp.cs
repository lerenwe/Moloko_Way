using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	CircleCollider2D m_Collider;
	GameObject m_player;
	bool isUsed = false;
    bool isPicked = false;
	Animator m_anim;
	GameObject m_environement;
	EnvironementManager m_environementManager;
	Transform m_position;
	Rigidbody2D m_rigidbody;
	Vector3 m_movement;
	Vector3 m_orbitationCenter;
	Orbitation m_orbitation;
	bool m_timeFlag = true;
	float deltaT = 0;
	int OndulationVariation = 500;

	// Use this for initialization
	void Start () {
	
		m_player = GameObject.FindGameObjectWithTag ("Spaceman");
		m_anim = GetComponent <Animator> ();
		m_environement = GameObject.Find ("EnvironementManager");
		m_rigidbody = GetComponent <Rigidbody2D> ();
		m_position = GetComponent <Transform> ();
		m_orbitation = GetComponent <Orbitation> ();
		m_orbitationCenter = m_orbitation.m_OrbitationCenter;
		Debug.Log (m_orbitation.m_OrbitationCenter);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 positionToRotationCenter = transform.position - m_orbitationCenter;
		positionToRotationCenter.z = 0;
		positionToRotationCenter.Normalize();

		deltaT += Time.deltaTime;



		if (deltaT >= 2) {
			deltaT = 0;
			m_timeFlag = !m_timeFlag;
		}



		/*if (m_timeFlag == true) {
			transform.position =  (transform.position + positionToRotationCenter * (deltaT/OndulationVariation));
				}
		else {
			transform.position = (transform.position - positionToRotationCenter * (deltaT/OndulationVariation));
		}*/
	}


    /// <summary>
    /// If the player got the pick up
    /// </summary>
    public void OnGetPickUp()
    {
		if (!isUsed)
        {
            m_anim.SetTrigger("IsUsed");
            m_environementManager = m_environement.GetComponent<EnvironementManager>();
            m_environementManager.nbPickUp = m_environementManager.nbPickUp - 1;
            //TO DO// Ajoute a la collection du Jouer-----------------------------------------------
            isUsed = true;
        }
    }

    public void OnPicked()
    {
        if (!isPicked)
        {
            m_anim.SetTrigger("IsPicked");
            isPicked = true;
        }
    }
	
	
}
