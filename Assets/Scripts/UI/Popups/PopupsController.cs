using System;
using System.Collections.Generic;
using System.Linq;
using UI.Base;
using UI.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public sealed class PopupsController : UIController<PopupsController>
    {
        [SerializeField] private Button blocker;
        
        [SerializeField] private RectTransform container;
        [SerializeField] private PopupData[] popupDataList;
        
        private Dictionary<PopupType, PopupBase> _popupsCache;

        private List<PopupType> _history;
        
        private PopupType _lastShown = PopupType.None;
        
        public event Action<PopupType> onPopupShow;
        public event Action<PopupType> onPopupHide;
        
        private void Awake()
        {
            _popupsCache = new Dictionary<PopupType, PopupBase>();
            _history = new List<PopupType>();
        }

        private void OnEnable()
        {
            blocker.onClick.AddListener(OnClickBlocker);
        }

        private void OnDisable()
        {
            blocker.onClick.RemoveListener(OnClickBlocker);
        }

        private void ShowBlocker(bool state, bool immediate = true)
        {
            blocker.gameObject.SetActive(state);
        }
        
        public void Show(PopupType popupType, bool immediate = false)
        {
            if (_lastShown == PopupType.None)
            {
                ShowBlocker(true, immediate);
            }
            
            _lastShown = popupType;
            
            var popupToShow = GetPopupInstance(popupType);
            
            popupToShow.Show(immediate);

            OnPopupShow(popupType);
        }

        public void Hide(bool immediate = false)
        {
            if(_lastShown == PopupType.None)
                return;
            
            var popup = GetPopupInstance(_lastShown);
            popup.Hide(immediate);
            
            OnPopupHide(_lastShown);

            if (_history.Count <= 0)
            {
                ShowBlocker(false, immediate);
                return;
            }
            
            _lastShown = _history[^1];
            _history.Remove(_lastShown);
        }
        
        private PopupBase GetPopupInstance(PopupType popupType)
        {
            if (_popupsCache.TryGetValue(popupType, out var popupBase)) return popupBase;
            
            var popupData = popupDataList.FirstOrDefault(x => x.PopupType == popupType);

            if (popupData == null)
            {
                throw new NullReferenceException($"{popupType} popup is not found!");
            }

            popupBase = Instantiate(popupData.PopupPrefab, container);
            _popupsCache.Add(popupType, popupBase);

            return popupBase;
        }

        private void OnClickBlocker()
        {
            Hide();
        }
        
        private void OnPopupShow(PopupType popupType)
        {
            onPopupShow?.Invoke(popupType);
        }

        private void OnPopupHide(PopupType popupType)
        {
            onPopupHide?.Invoke(popupType);
        }
    }
}