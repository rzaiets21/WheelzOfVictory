using System;
using System.Collections.Generic;
using System.Linq;
using MyOwn;
using UnityEngine;

namespace UI.Base
{
    public abstract class UIController<TEnum, TValue, T> : Singleton<T> where T : MonoBehaviour
                                                                        where TEnum : Enum
                                                                        where TValue : UIControl
    {
        [SerializeField] private RectTransform container;
        
        protected Dictionary<TEnum, TValue> _uiControlsCache;
        protected List<TEnum> _history;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _uiControlsCache = new Dictionary<TEnum, TValue>();
            _history = new List<TEnum>();
            
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        protected abstract UIControlData<TEnum, TValue>[] GetUIControlsDataList();
        
        protected TValue GetUIControlInstance(TEnum uiControlType)
        {
            if (_uiControlsCache.TryGetValue(uiControlType, out var uiControl)) return uiControl;
            
            var uiControlData = GetUIControlsDataList().FirstOrDefault(x => x.UIControlType.Equals(uiControlType));

            if (uiControlData == null)
            {
                throw new NullReferenceException($"{uiControlType} UIControl is not found!");
            }

            uiControl = Instantiate(uiControlData.UIControlPrefab, container);
            _uiControlsCache.Add(uiControlType, uiControl);

            return uiControl;
        }
    }
}