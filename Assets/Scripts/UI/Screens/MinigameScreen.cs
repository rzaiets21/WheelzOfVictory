using System;
using MyOwn;
using UI.Screens.Base;
using UI.Screens.Parts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Screens
{
    public class MinigameScreen : ScreenBase
    {
        [SerializeField] private MinigameCard[] cards;

        [SerializeField] private int sufflesCount;

        private int _shuffles;

        private void OnEnable()
        {
            foreach (var minigameCard in cards)
            {
                minigameCard.onClickCard += OnClickCard;
            }
        }

        private void OnDisable()
        {
            foreach (var minigameCard in cards)
            {
                minigameCard.onClickCard -= OnClickCard;
            }
        }

        protected void SetCardsInteract(bool state)
        {
            foreach (var minigameCard in cards)
            {
                minigameCard.SetInteract(state);
            }
        }
        
        [ContextMenu("Shuffle")]
        protected void StartShuffle()
        {
            _shuffles = 0;
            SetCardsInteract(false);

            Shuffle();
        }
        
        private void Shuffle()
        {
            var movingTime = 1f;
            
            var randomCardIndex = Random.Range(0, cards.Length);
            var randomCard = cards[randomCardIndex];

            var randomTargetCardIndex = Random.Range(0, cards.Length);
            while (randomCardIndex == randomTargetCardIndex)
            {
                randomTargetCardIndex = Random.Range(0, cards.Length);
            }

            var targetCard = cards[randomTargetCardIndex];

            var randomCardPosition = randomCard.CurrentPosition;
            var targetCardPosition = targetCard.CurrentPosition;

            randomCard.MoveToPosition(targetCardPosition, movingTime);
            targetCard.MoveToPosition(randomCardPosition, movingTime, CheckShuffleComplete);
            
            _shuffles++;
        }

        private void CheckShuffleComplete()
        {
            if (_shuffles >= sufflesCount)
            {
                ShuffleCompleted();
                return;
            }

            Shuffle();
        }
        
        private void ShuffleCompleted()
        {
            _shuffles = 0;
            SetCardsInteract(true);
            
            Debug.Log("Shuffle completed");
        }

        private void OnClickCard(int reward)
        {
            Debug.Log($"Get reward {reward} coins!");
            DailyBonusController.Instance.MarkDailyBonusIsReceived();
        }
    }
}