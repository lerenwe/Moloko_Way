using UnityEngine;
using System.Collections;

public class SelfRotation : MonoBehaviour
{

    [SerializeField]float m_RotationAngle = 5;
    [SerializeField]int   m_Direction = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*m_Direction,Time.deltaTime*m_RotationAngle);
    }
}
