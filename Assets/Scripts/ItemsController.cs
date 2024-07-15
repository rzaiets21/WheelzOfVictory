using System;
using MyOwn.Model;
using Newtonsoft.Json;
using UnityEngine;

namespace MyOwn
{
    public sealed class ItemsController : Singleton<ItemsController>
    {
        private const string EquippedItemsDataKey = "EquippedItemsDataKey";
        private const string BoughtItemsDataKey = "BoughtItemsDataKey";
        
        [SerializeField] private ItemsDatabase itemsDatabase;

        private EquippedItemsData _equippedItemsData;
        private BoughtItemsData _boughtItemsData;
        
        public void Awake()
        {
            LoadData();
        }

        private void LoadData()
        {
            _equippedItemsData = LoadEquippedItemsData();
            _boughtItemsData = LoadBoughtItemsData();
        }

        private EquippedItemsData LoadEquippedItemsData()
        {
            var rawJson = PlayerPrefs.GetString(EquippedItemsDataKey, string.Empty);

            return string.IsNullOrWhiteSpace(rawJson) ? new EquippedItemsData() 
                : JsonConvert.DeserializeObject<EquippedItemsData>(rawJson);
        }

        private BoughtItemsData LoadBoughtItemsData()
        {
            var rawJson = PlayerPrefs.GetString(BoughtItemsDataKey, string.Empty);

            return string.IsNullOrWhiteSpace(rawJson) ? new BoughtItemsData() 
                : JsonConvert.DeserializeObject<BoughtItemsData>(rawJson);
        }

        private void SaveEquippedItemsData()
        {
            var rawJson = JsonConvert.SerializeObject(_equippedItemsData);
            PlayerPrefs.SetString(EquippedItemsDataKey, rawJson);
        }

        private void SaveBoughtItemsData()
        {
            var rawJson = JsonConvert.SerializeObject(_boughtItemsData);
            PlayerPrefs.SetString(BoughtItemsDataKey, rawJson);
        }
        
        public ItemData[] GetItemsData(ItemType itemType) => itemsDatabase.GetItemsData(itemType);

        public void Equip(string itemId, ItemType itemType)
        {
            _equippedItemsData.Equip(itemId, itemType);
            SaveEquippedItemsData();
        }

        public bool IsEquipped(string itemId, ItemType itemType)
        {
            return _equippedItemsData.IsEquipped(itemId, itemType);
        }

        public bool IsBought(string itemId, ItemType itemType)
        {
            return _boughtItemsData.IsBought(itemId, itemType);
        }

        public void TryBuyItem(string itemId, Action onSuccess = null, Action onFail = null)
        {
            var itemData = itemsDatabase.GetItemData(itemId);
            var price = itemData.Price;
            var isSuccess = WalletController.Instance.TrySpend(price);

            if (!isSuccess)
            {
                onFail?.Invoke();
                return;
            }
            
            _boughtItemsData.Buy(itemId, itemData.ItemType);
            SaveBoughtItemsData();
            
            onSuccess?.Invoke();
        }
    }
}