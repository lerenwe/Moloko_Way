using UnityEngine;
using System.Collections;

public static class PlanetManager
{

	#region Members

		private static Planet m_Planet;

	#endregion


	#region Getters

		public static Planet GetPlanet()
		{
			return m_Planet;
		}

	#endregion


	#region Setters

		public static void SetPlanet(Planet planet)
		{
			m_Planet = planet;
		}

	#endregion

}