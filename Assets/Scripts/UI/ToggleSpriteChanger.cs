using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSpriteChanger : MonoBehaviour
    {
        [SerializeField] private ToggleSpriteChangerData toggleSpriteChangerData;

        private Toggle _toggle;

        private Toggle Toggle => _toggle ??= GetComponent<Toggle>();

        private void OnEnable()
        {
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDisable()
        {
            Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool value)
        {
            if(toggleSpriteChangerData == null || toggleSpriteChangerData.TargetGraphic == null)
                return;
            
            switch (value)
            {
                case true when !toggleSpriteChangerData.ActiveSprite:
                case false when !toggleSpriteChangerData.InactiveSprite:
                    return;
                default:
                    toggleSpriteChangerData.TargetGraphic.sprite =
                        value ? toggleSpriteChangerData.ActiveSprite : toggleSpriteChangerData.InactiveSprite;
                    break;
            }
        }
    }

    [Serializable]
    public class ToggleSpriteChangerData
    {
        [field: SerializeField] public Image TargetGraphic { get; private set; }
        [field: SerializeField] public Sprite ActiveSprite { get; private set; }
        [field: SerializeField] public Sprite InactiveSprite { get; private set; }
    }
}