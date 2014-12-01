using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironementManager : MonoBehaviour {
	
	public int nbSatelliteMax = 5;
	public int nbAsteroideMax = 5;
	public int nbPickUpMax = 5;
	public int nbAsteroid = 0;
	public int nbSatellite = 0;
	public int nbPickUp = 0;
	public GameObject[] spawnableObjects;
    public GameObject[] m_Pickups;
    public float m_SpeedMax = 200;
    public float m_SpeedMin = 10;
    public float m_PopAsteroideInterval = 5;
    public float m_PopPickUpInterval = 3;
    public float m_PopSatelliteInterval = 5;
    float deltaTimeSatellite = 0;
	float deltaTimeAsteroide = 0;
    float deltaTimePickUp = 0;
	GameObject satellite;
	GameObject smallAsteroide;
	GameObject mediumAsteroide;
	GameObject bigAsteroide;
	GameObject pickUp;
	GameSettings gamesettings;
	
	// Use this for initialization
	void Start () {
		
		satellite = spawnableObjects[0];
		smallAsteroide = spawnableObjects [1];
		mediumAsteroide = spawnableObjects [2];
		bigAsteroide = spawnableObjects [3];        
		pickUp = m_Pickups [Random.Range(0,m_Pickups.Length)];

		gamesettings = GameManager.GetGameSettings();
	}
	
	// Update is called once per frame
	void Update () {
		
		deltaTimeAsteroide += Time.deltaTime;
        deltaTimePickUp += Time.deltaTime;
        deltaTimeSatellite += Time.deltaTime;
		
		
		if (deltaTimeAsteroide >= m_PopAsteroideInterval) 
        {
			deltaTimeAsteroide = 0;					
			Spawn ("Asteroide");
		}

        if (deltaTimeSatellite >= m_PopSatelliteInterval)
        {
            deltaTimeSatellite = 0;
            Spawn("Satellite");	
        }

        if(deltaTimePickUp>=m_PopPickUpInterval)
        {
            deltaTimePickUp = 0;
            Spawn("PickUp");
        }
	}
	
	
	void Spawn (string NameSpawn) {
		
		Vector3 spawnPoint = Vector3.zero;
		GameObject I;
		
		switch(Random.Range(0,4)){
		case 0:
			spawnPoint.Set (Random.Range(gamesettings.GetMinOrbitation(),gamesettings.GetMaxOrbitation()),0f, 0f);
			break;
		case 1:
			spawnPoint.Set (-Random.Range(gamesettings.GetMinOrbitation(),gamesettings.GetMaxOrbitation()),0f, 0f);
			break;
		case 2:
			spawnPoint.Set (0f,Random.Range(gamesettings.GetMinOrbitation(),gamesettings.GetMaxOrbitation()), 0f);
			break;
		case 3:
			spawnPoint.Set (0f,-Random.Range(gamesettings.GetMinOrbitation(),gamesettings.GetMaxOrbitation()), 0f);
			break;
		}

        if (NameSpawn == "Satellite" && nbSatellite<nbSatelliteMax)
        {
            Instantiate(satellite, spawnPoint, transform.rotation);
            nbSatellite++;
        }
        else if (NameSpawn == "PickUp" && nbPickUp<nbPickUpMax)
        {
            Instantiate(pickUp, spawnPoint, transform.rotation);
            nbPickUp++;
        }
        else if (NameSpawn == "Asteroide" && nbAsteroid<nbAsteroideMax)
        {
            switch (Random.Range(0, 3))
            {
                case 1:
                    I = (GameObject)Instantiate(smallAsteroide, spawnPoint, transform.rotation);
                    I.GetComponent<Orbitation>().m_OrbitationSpeed = Random.Range(m_SpeedMin, m_SpeedMax);

                    break;
                case 2:
                    I = (GameObject)Instantiate(mediumAsteroide, spawnPoint, transform.rotation);
                    I.GetComponent<Orbitation>().m_OrbitationSpeed = Random.Range(m_SpeedMin, m_SpeedMax);
                    break;
                case 3:
                    I = (GameObject)Instantiate(bigAsteroide, spawnPoint, transform.rotation);
                    I.GetComponent<Orbitation>().m_OrbitationSpeed = Random.Range(m_SpeedMin, m_SpeedMax);
                    break;
            }
            nbAsteroid++;
        }
	}
	
}
