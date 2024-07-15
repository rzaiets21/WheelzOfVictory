using System;
using UnityEngine;

namespace MyOwn.Model
{
    [Serializable]
    public sealed class WeaponData : ItemData
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        
        public WeaponData SetWeaponType(WeaponType weaponType)
        {
            WeaponType = weaponType;
            return this;
        }
    }
}