using UnityEngine;
using System.Collections;

public static class ScoreManager
{

    #region Members

        private static int m_Score = 0;

    #endregion


    #region Getters

        public static int GetScore()
        {
            return m_Score;
        }

    #endregion


    #region Setters

        public static void AddScore(int score)
        {
            m_Score += score;
        }

    #endregion

}
