using System;
using MyOwn;
using UnityEngine;

namespace UI.Base
{
    public abstract class UIController<T> : Singleton<T> where T : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}