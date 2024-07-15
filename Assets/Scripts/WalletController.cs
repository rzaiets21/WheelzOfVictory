using System;
using UnityEngine;

namespace MyOwn
{
    public sealed class WalletController : Singleton<WalletController>
    {
        private const string CurrencyKey = "CurrentCoins";

        private int _currentCoins;

        public int Coins
        {
            get => _currentCoins;
            private set
            {
                _currentCoins = value;
                OnCoinsValueChanged(_currentCoins);
            }
        }
        
        public event Action<int> onCoinsValueChanged;

        private void Awake()
        {
            Load();
        }

        public bool TrySpend(int amount)
        {
            if (amount > Coins)
                return false;

            Coins -= amount;
            return true;
        }
        
        private void Load()
        {
            Coins = PlayerPrefs.GetInt(CurrencyKey, 0);
        }
        
        private void Save()
        {
            PlayerPrefs.SetInt(CurrencyKey, Coins);
        }
        
        private void OnCoinsValueChanged(int value)
        {
            Save();
            
            onCoinsValueChanged?.Invoke(value);
        }
    }
}