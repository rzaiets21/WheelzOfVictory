using DG.Tweening;
using MyOwn;
using UI.Screens.Base;
using UnityEngine;

namespace UI.Screens
{
    public sealed class LoadingScreen : ScreenBase
    {
        private const float AnimationStartDelay = 0.75f;
        private const float ShowNextScreenDelay = 0.75f;
        
        [SerializeField] private Animator animator;

        protected override void OnShown()
        {
            DOVirtual.DelayedCall(AnimationStartDelay, () =>
            {
                animator.SetTrigger("Loading");
            });
        }

        private void OnAnimationCompleted()
        {
            Debug.Log("AnimationCompleted");
            DOVirtual.DelayedCall(ShowNextScreenDelay, () =>
            {
                var dailyBonusIsReady = DailyBonusController.Instance.DailyBonusIsReady;
                ScreensController.Instance.Show(dailyBonusIsReady ? ScreenType.Minigame : ScreenType.MainMenu);
            });
        }
    }
}