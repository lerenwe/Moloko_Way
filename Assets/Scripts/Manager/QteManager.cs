using UnityEngine;
using System.Collections;

public static class QteManager
{

    #region Members

        private static QteUI m_QteUI;

    #endregion


    #region Setters

        public static void SetQteUI(QteUI qteUI)
        {
            m_QteUI = qteUI;
        }

    #endregion


    #region Getters

        public static QteUI GetQteUI()
        {
            return m_QteUI;
        }

    #endregion

}
