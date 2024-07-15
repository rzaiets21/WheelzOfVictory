using MyOwn.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Parts
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image image;

        private ItemData _itemData;

        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();

        public string ItemId => _itemData.ID;
        public ItemType ItemType => _itemData.ItemType;
        
        public void Init(ItemData itemData)
        {
            _itemData = itemData;
            image.sprite = _itemData.Sprite;
        }
    }
}