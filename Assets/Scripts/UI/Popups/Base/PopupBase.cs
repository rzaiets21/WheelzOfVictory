using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups.Base
{
    public abstract class PopupBase : UIControl
    {
        [SerializeField] private Button closeButton;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonClick);
            OnEnabled();
        }

        protected virtual void OnEnabled() { }
        
        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClick);
            OnDisabled();
        }
        
        protected virtual void OnDisabled() { }

        private void OnCloseButtonClick()
        {
            PopupsController.Instance.Hide();
        }
    }
}