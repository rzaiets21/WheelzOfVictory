using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Parts
{
    public class MinigameCard : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private int rewardCount;

        public event Action<int> onClickCard;
        
        public Vector2 CurrentPosition => rectTransform.anchoredPosition;

        private void OnEnable()
        {
            button.onClick.AddListener(OnClickCard);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClickCard);
        }

        public void SetInteract(bool state)
        {
            button.interactable = state;
        }
        
        public Tween MoveToPosition(Vector2 position, float duration, Action onComplete = null)
        {
            return rectTransform.DOAnchorPos(position, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        private void OnClickCard()
        {
            onClickCard?.Invoke(rewardCount);
        }
    }
}