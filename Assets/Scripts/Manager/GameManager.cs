using UnityEngine;
using System.Collections;

public static class GameManager
{

	#region Members

		private static GameSettings m_GameSettings;
		private static bool m_GameOver = false;

	#endregion


	#region Checkers

		public static bool IsGameOver()
		{
			return m_GameOver;
		}

	#endregion


	#region Getters

		public static GameSettings GetGameSettings()
		{
			return m_GameSettings;
		}

	#endregion


	#region Setters

		public static void SetGameOver()
		{
			m_GameOver = true;
			Application.LoadLevel ("Menu");
		}

		public static void SetGameSettings(GameSettings gameSettings)
		{
			m_GameSettings = gameSettings;
		}

	#endregion

}
