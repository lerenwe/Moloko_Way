using UnityEngine;
using System.Collections;

public class LeftClick : MonoBehaviour
{

    void Awake()
    {
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

}
