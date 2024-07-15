using System;
using UI.Popups.Base;
using UnityEngine;

namespace UI.Popups
{
    [Serializable]
    public class PopupData
    {
        [field: SerializeField] public PopupBase PopupPrefab { get; private set; }
        [field: SerializeField] public PopupType PopupType { get; private set; }
    }
    
    public enum PopupType
    {
        None = 0,
        NotEnoughCoins = 1,
        Win = 2,
        Defeat = 3,
    }
}