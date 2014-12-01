using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour
{

    #region GUI

        void OnGUI()
        {
            GUI.Box(new Rect(400, 5, 100, 24), ScoreManager.GetScore() + "");
        }

    #endregion

}
