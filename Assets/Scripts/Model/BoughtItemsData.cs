using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyOwn.Model
{
    [Serializable]
    public class BoughtItemsData
    {
        [JsonProperty("boughtItems")] private Dictionary<ItemType, List<string>> BoughtItems = new();

        public void Buy(string id, ItemType itemType)
        {
            if (!BoughtItems.TryGetValue(itemType, out var list))
            {
                list = new List<string>();
                BoughtItems.Add(itemType, list);
            }

            list.Add(id);
        }

        public bool IsBought(string id, ItemType itemType)
        {
            return BoughtItems.TryGetValue(itemType, out var list) && list.Contains(id);
        }
    }
}