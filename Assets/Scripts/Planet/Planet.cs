using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{

	void Awake()
	{
		PlanetManager.SetPlanet (this);
	}

}
