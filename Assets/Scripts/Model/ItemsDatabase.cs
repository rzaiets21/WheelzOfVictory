using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyOwn.Model
{
    [CreateAssetMenu(menuName = "Create/Items/Database", fileName = "ItemsDatabase")]
    public sealed class ItemsDatabase : ScriptableObject
    {
        [SerializeField, SerializeReference] private List<ItemData> items = new List<ItemData>();
        
        public ItemData[] GetItemsData(ItemType itemType) => items.Where(x => x.ItemType == itemType).ToArray();

        public ItemData GetItemData(string id) => items.FirstOrDefault(x => x.ID == id);
    }
}