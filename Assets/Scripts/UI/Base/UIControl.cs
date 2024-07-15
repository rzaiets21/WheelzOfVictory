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
                OnShown();
            
                onComplete?.Invoke();
                return;
            }
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
                OnHidden();
                onComplete?.Invoke();
                return;
            }
        }

        protected virtual void OnHidden()
        {
            
        }
    }
}