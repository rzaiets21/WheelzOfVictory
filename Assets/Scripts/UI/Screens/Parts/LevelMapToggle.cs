using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Parts
{
    public sealed class LevelMapToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggleComponent;
        [SerializeField] private TextMeshProUGUI levelText;

        public event Action<int> onSelect;

        private bool _isSelected;
        private int _levelIndex;

        private float _scaleDuration = 0.5f;
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform ??= GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            toggleComponent.onValueChanged.AddListener(OnSelect);
        }

        private void OnDisable()
        {
            toggleComponent.onValueChanged.RemoveListener(OnSelect);
        }

        public LevelMapToggle SetPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
            return this;
        }
        
        public void Init(int levelIndex, ToggleGroup toggleGroup)
        {
            _levelIndex = levelIndex;
            levelText.SetText($"Level {_levelIndex + 1}");
            toggleComponent.group = toggleGroup;
        }
        
        private void OnSelect(bool value)
        {
            if(_isSelected == value)
                return;
            
            _isSelected = value;

            _rectTransform.DOScale(_isSelected ? 1.5f : 1f, _scaleDuration);
            
            onSelect?.Invoke(_levelIndex);
        }
    }
}