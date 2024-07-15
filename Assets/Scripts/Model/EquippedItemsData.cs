using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace MyOwn.Model
{
    [Serializable]
    public class EquippedItemsData
    {
        [JsonProperty("equippedWeaponId")] private string EquippedWeaponId;
        [JsonProperty("equippedItems")] private Dictionary<ItemType, List<string>> EquippedItems = new();

        public bool HasEquipped(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => !string.IsNullOrEmpty(EquippedWeaponId),
                ItemType.Upgrade => EquippedItems.TryGetValue(itemType, out var list) && list.Count > 0,
                _ => throw new NotImplementedException()
            };
        }
        
        public bool IsEquipped(string id, ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => EquippedWeaponId == id,
                ItemType.Upgrade => EquippedItems.TryGetValue(itemType, out var list) && list.Contains(id),
                _ => throw new NotImplementedException()
            };
        }
        
        public void Equip(string id, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    EquippedWeaponId = id;
                    break;
                case ItemType.Upgrade:
                    if (!EquippedItems.TryGetValue(itemType, out var list))
                    {
                        list = new List<string>();
                        EquippedItems.Add(itemType, list);
                    }
                    
                    list.Add(id);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}