using System.Collections.Generic;
using DG.Tweening;
using MyOwn;
using MyOwn.Model;
using TMPro;
using UI.Popups;
using UI.Screens.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Base
{
    public abstract class ScreenWithScrollItems : ScreenBase
    {
        private const float ScrollDuration = 0.25f;
        
        [SerializeField] private TextMeshProUGUI currencyText;
        
        [SerializeField] private ShopItem itemPrefab;
        [SerializeField] private RectTransform container;
        
        [SerializeField] private Button rightButton;
        [SerializeField] private Button leftButton;

        [SerializeField] private Button useButton;
        [SerializeField] private Button buyButton;

        private int _selectedItem = 0;
        private bool _isScrolling;
        
        private List<ShopItem> _shopItems = new List<ShopItem>();

        public ShopItem SelectedShopItem => _shopItems[_selectedItem];
        
        private void Awake()
        {
            Init(GetItems());
        }

        private void OnEnable()
        {
            leftButton.onClick.AddListener(OnClickLeftButton);
            rightButton.onClick.AddListener(OnClickRightButton);
            
            useButton.onClick.AddListener(OnClickUseButton);
            buyButton.onClick.AddListener(OnClickBuyButton);

            WalletController.Instance.onCoinsValueChanged += SetCurrencyText;
        }

        private void OnDisable()
        {
            leftButton.onClick.RemoveListener(OnClickLeftButton);
            rightButton.onClick.RemoveListener(OnClickRightButton);
            
            useButton.onClick.RemoveListener(OnClickUseButton);
            buyButton.onClick.RemoveListener(OnClickBuyButton);
            
            WalletController.Instance.onCoinsValueChanged -= SetCurrencyText;
        }

        private void Init(ItemData[] items)
        {
            var length = items.Length;
            for (int i = 0; i < length; i++)
            {
                var item = Instantiate(itemPrefab, container);
                var itemData = items[i];
                item.Init(itemData);

                item.RectTransform.anchoredPosition =
                    i == 0
                        ? Vector2.zero
                        : new Vector2(container.rect.width, 0);
                
                _shopItems.Add(item);
            }

            SetCurrencyText(WalletController.Instance.Coins);
        }

        private void SetCurrencyText(int amount)
        {
            currencyText.text = amount.ToString();
        }
        
        protected abstract ItemData[] GetItems();

        private void UpdateNavigateButtonsVisibility()
        {
            leftButton.gameObject.SetActive(_selectedItem > 0);
            rightButton.gameObject.SetActive(_selectedItem < _shopItems.Count - 1);
        }

        private void UpdateInteractButtons()
        {
            var isBought = ItemsController.Instance.IsBought(SelectedShopItem.ItemId, SelectedShopItem.ItemType);
            var isEquipped = ItemsController.Instance.IsEquipped(SelectedShopItem.ItemId, SelectedShopItem.ItemType);

            useButton.interactable = !isEquipped;
            useButton.gameObject.SetActive(isBought);
            buyButton.gameObject.SetActive(!isBought);
        }

        private void HideInteractButtons()
        {
            useButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }

        private void Scroll(bool right)
        {
            _isScrolling = true;
            
            HideInteractButtons();

            var currentItem = _shopItems[_selectedItem];
            _selectedItem += right ? 1 : -1;
            var targetItem = _shopItems[_selectedItem];
            
            targetItem.RectTransform.anchoredPosition = new Vector2(container.rect.width * (right ? 1 : -1), 0);

            var currentItemPosition = currentItem.RectTransform.anchoredPosition;
            var targetItemPosition = new Vector2(container.rect.width * (right ? -1 : 1), 0);
            
            targetItem.RectTransform.DOAnchorPos(currentItemPosition, ScrollDuration).SetEase(Ease.InOutCubic);
            currentItem.RectTransform.DOAnchorPos(targetItemPosition, ScrollDuration).SetEase(Ease.InOutCubic).OnComplete(
                () =>
                {
                    _isScrolling = false;
                    UpdateNavigateButtonsVisibility();
                    UpdateInteractButtons();
                });
        }
        
        private void OnClickLeftButton()
        {
            if(_isScrolling)
                return;
            
            if(_selectedItem <= 0)
                return;

            Scroll(false);
        }

        private void OnClickRightButton()
        {
            if(_isScrolling)
                return;
            
            if(_selectedItem >= _shopItems.Count - 1)
                return;
            
            Scroll(true);
        }
        
        private void OnClickUseButton()
        {
            ItemsController.Instance.Equip(SelectedShopItem.ItemId, SelectedShopItem.ItemType);
            
            UpdateInteractButtons();
        }

        private void OnClickBuyButton()
        {
            ItemsController.Instance.TryBuyItem(SelectedShopItem.ItemId, UpdateInteractButtons, () =>
            {
                PopupsController.Instance.Show(PopupType.NotEnoughCoins);
            });
        }
    }
}