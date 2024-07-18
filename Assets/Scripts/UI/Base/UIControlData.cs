using System;
using UnityEngine;

namespace UI.Base
{
    [Serializable]
    public abstract class UIControlData<TEnum, TValue> where TEnum : Enum
                                                       where TValue : UIControl
    {
        [field: SerializeField] public TValue UIControlPrefab { get; private set; }
        [field: SerializeField] public TEnum UIControlType { get; private set; }
    }
}