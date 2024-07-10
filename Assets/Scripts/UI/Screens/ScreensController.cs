using System;
using System.Collections.Generic;
using System.Linq;
using MyOwn;
using UI.Screens.Base;
using UnityEngine;

namespace UI.Screens
{
    public sealed class ScreensController : Singleton<ScreensController>
    {
        [SerializeField] private ScreenType screenOnStart;
        
        [SerializeField] private RectTransform container;
        [SerializeField] private ScreenData[] screenDatas;

        private ScreenType _currentScreen = ScreenType.None;

        private Dictionary<ScreenType, ScreenBase> _screensCache;
        
        public event Action<ScreenType> onScreenShow;
        public event Action<ScreenType> onScreenHide;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _screensCache = new Dictionary<ScreenType, ScreenBase>();
        }

        private void Start()
        {
            Show(screenOnStart);
        }

        public void Show(ScreenType screenType, bool immediate = false)
        {
            var screenToShow = GetScreenInstance(screenType);
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
            
            var screen = GetScreenInstance(screenType);
            screen.Hide(immediate);
            
            OnScreenHide(screenType);
        }
        
        private ScreenBase GetScreenInstance(ScreenType screenType)
        {
            if (_screensCache.TryGetValue(screenType, out var screenBase)) return screenBase;
            
            var screenData = screenDatas.FirstOrDefault(x => x.ScreenType == screenType);

            if (screenData == null)
            {
                throw new NullReferenceException($"{screenType} screen is not found!");
            }

            screenBase = Instantiate(screenData.ScreenPrefab, container);
            _screensCache.Add(screenType, screenBase);

            return screenBase;
        }

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