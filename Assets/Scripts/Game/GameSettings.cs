using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
	
	#region Members
	
	[SerializeField]	private float m_DistanceMinOrbitation=5;
	[SerializeField]	private float m_DistanceMaxOrbitation=10;
	[SerializeField]    private float m_DistanceToCrash = 5;
	[SerializeField]    private float m_OrbitationDistanceAccelerationFactor=10;
	
	#endregion
	
	
	#region Initialisation
	
	void Awake()
	{
		GameManager.SetGameSettings(this);
	}
	
	#endregion
	
	
	#region Getters
	
	public float GetDistanceToGameOver()
	{
		return m_DistanceMaxOrbitation;
	}
	
	public float GetMinOrbitation()
	{
		return m_DistanceMinOrbitation;
	}
	
	public float GetMaxOrbitation()
	{
		return m_DistanceMaxOrbitation;
	}
	
	public float GetOrbitationDistanceAccelerationFactor()
	{
		return m_OrbitationDistanceAccelerationFactor;
	}
	
	public float GetDistanceToCrash()
	{
		return m_DistanceToCrash;
	}
	
	#endregion
	
}
