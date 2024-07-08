using DG.Tweening;
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
                ScreensController.Instance.Show(ScreenType.MainMenu);
            });
        }
    }
}