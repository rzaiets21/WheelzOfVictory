using System;
using UnityEngine;

namespace UI.Base
{
    public abstract class UIControl : MonoBehaviour
    {
        public void Show(bool immediate, Action onComplete = null)
        {
            ShowImplementation(immediate, onComplete);
        }

        protected virtual void ShowImplementation(bool immediate, Action onComplete = null)
        {
            if (immediate)
            {
                gameObject.SetActive(true);
                OnShown();
            
                onComplete?.Invoke();
                return;
            }
            
            gameObject.SetActive(true);
            OnShown();
            
            onComplete?.Invoke();
        }
        
        protected virtual void OnShown()
        {
            
        }
        
        public void Hide(bool immediate, Action onComplete = null)
        {
            HideImplementation(immediate, onComplete);
        }

        protected virtual void HideImplementation(bool immediate, Action onComplete = null)
        {
            if (immediate)
            {
                gameObject.SetActive(false);
                OnHidden();
                onComplete?.Invoke();
                return;
            }
            
            gameObject.SetActive(false);
            OnHidden();
            onComplete?.Invoke();
        }

        protected virtual void OnHidden()
        {
            
        }
    }
}