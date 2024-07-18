using System;
using UI.Base;
using UI.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public sealed class PopupsController : UIController<PopupType, PopupBase, PopupsController>
    {
        [SerializeField] private PopupData[] popupsDataList;
        [SerializeField] private Button blocker;
        
        private PopupType _lastShown = PopupType.None;
        
        public event Action<PopupType> onPopupShow;
        public event Action<PopupType> onPopupHide;

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
            
            var popupToShow = GetUIControlInstance(popupType);
            
            popupToShow.Show(immediate);

            _history.Add(_lastShown);
            
            OnPopupShow(popupType);
        }

        public void Hide(bool immediate = false)
        {
            if(_lastShown == PopupType.None)
                return;
            
            var popup = GetUIControlInstance(_lastShown);
            popup.Hide(immediate);
            
            _history.Remove(_lastShown);
            _lastShown = _history.Count > 0 ? _history[^1] : PopupType.None;
            
            if (_history.Count <= 0)
            {
                ShowBlocker(false, immediate);
            }
            
            OnPopupHide(_lastShown);
            
        }

        protected override UIControlData<PopupType, PopupBase>[] GetUIControlsDataList() => popupsDataList;
        
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