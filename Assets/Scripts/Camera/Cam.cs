using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    [SerializeField]float m_DistanceToCenterDezoom=8;
    [SerializeField]float m_MaxZoom=30;
    [SerializeField]float m_MinZoom=10;
    [SerializeField]float m_ZoomSpeed=2f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(PlayersManager.m_Spaceman.transform.position,Vector3.zero);
       /* if (Vector3.Distance(PlayersManager.m_Spaceship.transform.position, Vector3.zero) > distance)
        {
            distance = Vector3.Distance(PlayersManager.m_Spaceship.transform.position, Vector3.zero);
        }*/

        Debug.Log(" Cam distance = " + distance);

       
        float size = (distance - m_DistanceToCenterDezoom)*100 * m_ZoomSpeed;

        Debug.Log(" size = " + size);

        if(size<m_MinZoom)
        {
            size=m_MinZoom;
        }
        else if(size>m_MaxZoom)
        {
            size = m_MaxZoom;
        }

        camera.orthographicSize = size;
            
        
    }
}
