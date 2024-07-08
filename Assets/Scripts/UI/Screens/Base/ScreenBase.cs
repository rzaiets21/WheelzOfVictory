using System;
using UnityEngine;

namespace UI.Screens.Base
{
    public abstract class ScreenBase : MonoBehaviour
    {
        public void Show(bool immediate, Action onComplete = null)
        {
            OnShown();
            
            onComplete?.Invoke();
        }

        protected virtual void OnShown()
        {
            
        }
        
        public void Hide(bool immediate, Action onComplete = null)
        {
            OnHidden();
            onComplete?.Invoke();
        }

        protected virtual void OnHidden()
        {
            
        }
    }
}