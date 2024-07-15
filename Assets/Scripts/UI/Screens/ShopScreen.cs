using System;
using UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public sealed class ShopScreen : ScreenBase
    {
        [SerializeField] private Button upgradeSkillButton;
        [SerializeField] private Button weaponButton;

        private void OnEnable()
        {
            upgradeSkillButton.onClick.AddListener(OnClickUpgradeSkillButton);
            weaponButton.onClick.AddListener(OnClickWeaponButton);
        }

        private void OnDisable()
        {
            upgradeSkillButton.onClick.RemoveListener(OnClickUpgradeSkillButton);
            weaponButton.onClick.RemoveListener(OnClickWeaponButton);
        }

        private void OnClickUpgradeSkillButton()
        {
            ScreensController.Instance.Show(ScreenType.UpgradeSkill);
        }

        private void OnClickWeaponButton()
        {
            ScreensController.Instance.Show(ScreenType.Weapon);
        }
    }
}