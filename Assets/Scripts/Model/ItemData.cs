using System;
using UnityEngine;

namespace MyOwn.Model
{
    [Serializable]
    public class ItemData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        public ItemData SetName(string name)
        {
            Name = name;
            return this;
        }

        public ItemData SetID(string id)
        {
            ID = id;
            return this;
        }

        public ItemData SetSprite(Sprite sprite)
        {
            Sprite = sprite;
            return this;
        }

        public ItemData SetItemType(ItemType itemType)
        {
            ItemType = itemType;
            return this;
        }

        public ItemData SetPrice(int price)
        {
            Price = price;
            return this;
        }
    }
}