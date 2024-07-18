using System;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class TopPanel : MonoBehaviour
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Button closeButton;

        private void OnEnable()
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
            
            closeButton.onClick.AddListener(OnClickCloseButton);
        }

        private void OnDisable()
        {
            musicToggle.onValueChanged.RemoveListener(OnMusicToggleValueChanged);
            
            closeButton.onClick.RemoveListener(OnClickCloseButton);
        }

        private void OnMusicToggleValueChanged(bool value)
        {
            
        }

        private void OnClickCloseButton()
        {
            ScreensController.Instance.Previous(true);
        }
    }
}