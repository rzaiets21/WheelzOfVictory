using System;
using UI.Base;
using UI.Screens.Base;
using UnityEngine;

namespace UI.Screens
{
    public sealed class ScreensController : UIController<ScreenType, ScreenBase, ScreensController>
    {
        [SerializeField] private ScreenData[] screensDataList;
        [SerializeField] private ScreenType screenOnStart;
        
        private ScreenType _currentScreen = ScreenType.None;

        public event Action<ScreenType> onScreenShow;
        public event Action<ScreenType> onScreenHide;

        private void Start()
        {
            Show(screenOnStart, true);
        }

        public void Show(ScreenType screenType, bool immediate = false)
        {
            var screenToShow = GetUIControlInstance(screenType);
            var screenToHide = _currentScreen;
            
            screenToShow.Show(immediate, () =>
            {
                Hide(screenToHide, true);
            });

            OnScreenShow(screenType);
        }

        public void Hide(ScreenType screenType, bool immediate = false)
        {
            if(screenType == ScreenType.None)
                return;
            
            var screen = GetUIControlInstance(screenType);
            screen.Hide(immediate);
            
            OnScreenHide(screenType);
        }

        public void Previous(bool immediate = false)
        {
            Hide(_currentScreen, immediate);
        }

        protected override UIControlData<ScreenType, ScreenBase>[] GetUIControlsDataList() => screensDataList;

        private void OnScreenShow(ScreenType screenType)
        {
            onScreenShow?.Invoke(screenType);
        }

        private void OnScreenHide(ScreenType screenType)
        {
            onScreenHide?.Invoke(screenType);
        }
    }
}