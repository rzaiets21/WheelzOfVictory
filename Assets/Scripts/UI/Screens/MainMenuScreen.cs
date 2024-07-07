using System;
using UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public sealed class MainMenuScreen : ScreenBase
    {
        [SerializeField] private Button infoButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button giftButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button mapButton;
        [SerializeField] private Button faqButton;
        [SerializeField] private Toggle musicToggle;

        private void OnEnable()
        {
            infoButton.onClick.AddListener(OnClickInfoButton);
            shopButton.onClick.AddListener(OnClickShopButton);
            giftButton.onClick.AddListener(OnClickGiftButton);
            exitButton.onClick.AddListener(OnClickExitButton);
            mapButton.onClick.AddListener(OnClickMapButton);
            faqButton.onClick.AddListener(OnClickFaqButton);
            
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        }

        private void OnDisable()
        {
            infoButton.onClick.RemoveListener(OnClickInfoButton);
            shopButton.onClick.RemoveListener(OnClickShopButton);
            giftButton.onClick.RemoveListener(OnClickGiftButton);
            exitButton.onClick.RemoveListener(OnClickExitButton);
            mapButton.onClick.RemoveListener(OnClickMapButton);
            faqButton.onClick.RemoveListener(OnClickFaqButton);
            
            musicToggle.onValueChanged.RemoveListener(OnMusicToggleValueChanged);
        }

        private void OnClickInfoButton()
        {
            
        }

        private void OnClickShopButton()
        {
            
        }

        private void OnClickGiftButton()
        {
            
        }

        private void OnClickExitButton()
        {
            Application.Quit();
        }

        private void OnClickMapButton()
        {
            
        }

        private void OnClickFaqButton()
        {
            
        }

        private void OnMusicToggleValueChanged(bool value)
        {
            
        }
    }
}