using System;
using System.Collections;
using MyOwn;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public sealed class DailyBonusScreen : MinigameScreen
    {
        [SerializeField] private GameObject timerObject;
        [SerializeField] private TextMeshProUGUI timerText;

        protected override void OnShown()
        {
            base.OnShown();

            var dailyBonusIsReady = DailyBonusController.Instance.DailyBonusIsReady;
            if (dailyBonusIsReady)
            {
                SetCardsInteract(false);
                return;
            }

            StartCoroutine(TimerCoroutine());
        }

        private void HideTimer()
        {
            timerObject.SetActive(false);
        }
        
        private IEnumerator TimerCoroutine()
        {
            var targetDate = DateTime.Today + new TimeSpan(1, 0, 0, 0);
            var isCompleted = targetDate <= DateTime.Now;

            while (!isCompleted)
            {
                var timeSpan = targetDate - DateTime.Now;
                timerText.text = $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
                yield return new WaitForSeconds(1f);
                
                isCompleted = targetDate <= DateTime.Now;
            }

            HideTimer();
            StartShuffle();
        }
    }
}