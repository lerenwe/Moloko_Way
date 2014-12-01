using UnityEngine;
using System.Collections;

public class Orbitation : MonoBehaviour
{

    #region Members

        public  float       m_OrbitationSpeed=10;
        public Vector3      m_OrbitationCenter = new Vector3(0, 0, 1);
        public GameObject   m_OrbitatedObject;
        Vector3             m_PreviousVector;
    
    #endregion


    #region Monobehaviours Manipulators

        // Use this for initialization
        void Start()
        {
            m_OrbitationCenter.z=transform.position.z;
            if (m_OrbitatedObject != null)
            {
                m_PreviousVector = m_OrbitatedObject.transform.position - transform.position;
            }
        }

        // Update is called once per frame
        /// <summary>
        /// Rotate the position around the rotation center by orbitation speed by frame
        /// </summary>
        void Update()
        {
            float actualSpeed = 0;
            if (m_OrbitatedObject != null)
            {
                //Set the orbitation center to be, the actual position of the orbited object
                m_OrbitationCenter = m_OrbitatedObject.transform.position;
                actualSpeed = Time.deltaTime * m_OrbitationSpeed;               
                transform.position = m_OrbitationCenter - m_PreviousVector;
            }
            else
            {
                actualSpeed = Time.deltaTime * m_OrbitationSpeed * ((float)1f / Vector3.Distance(transform.position, m_OrbitationCenter) * GameManager.GetGameSettings().GetOrbitationDistanceAccelerationFactor());//the more closer to the planet, the faster
            }
           transform.up = (transform.position-m_OrbitationCenter).normalized;
            Vector3 positionToRotationCenter = transform.position - m_OrbitationCenter;
            
            Vector3 rotatedPosition = Quaternion.AngleAxis(actualSpeed, Vector3.forward) * positionToRotationCenter;
            transform.position = m_OrbitationCenter + rotatedPosition;

            if (m_OrbitatedObject != null)
            {
                m_PreviousVector = m_OrbitationCenter - transform.position;
            }
            
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_OrbitationCenter, 0.5f);
        }

    #endregion
}
