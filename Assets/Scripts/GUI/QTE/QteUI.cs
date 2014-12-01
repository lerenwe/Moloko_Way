using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QteUI : MonoBehaviour
{

    #region Members
    
                            private     RightClick          m_RightClick;
                            private     LeftClick           m_LeftClick;
       

    #endregion


    #region Initialisation

        void Awake()
        {
            QteManager.SetQteUI(this);
        }

        void Start()
        {
            m_RightClick = GetComponentInChildren<RightClick>();
            m_LeftClick = GetComponentInChildren<LeftClick>();

            ResetAnimators();
        }

    #endregion


    #region Checkers

        public bool IsLeftClickActive()
        {
            return m_LeftClick.gameObject.activeSelf;
        }

        public bool IsRightClickActive()
        {
            return m_RightClick.gameObject.activeSelf;
        }

    #endregion


    #region Setters

        public void SetLeftClickAnimator(bool state)
        {
            m_LeftClick.gameObject.SetActive(state);
        }

        public void SetRightClickAnimator(bool state)
        {
            m_RightClick.gameObject.SetActive(state);
        }

        public void ResetAnimators()
        {
            m_RightClick.gameObject.SetActive(false);
            m_LeftClick.gameObject.SetActive(false);
        }

    #endregion

}
