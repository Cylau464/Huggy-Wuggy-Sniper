using System;
using System.Collections;
using System.Collections.Generic;
using main.level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace Extension
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private ShopSettings _shopSettings;
        [SerializeField] private WeaponSettings _weaponSettings;
        
        [Header("POWER")]
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _btnUpgrade;
        [SerializeField] private TMP_Text _numberLevel;

        [Header("PROFIT")] 
        [SerializeField] private TMP_Text _priceTextProfit;
        [SerializeField] private Button _btnUpgradeProfit;
        [SerializeField] private TMP_Text _numberLevelProfit;

        [SerializeField] private List<GameObject> _allShops = new List<GameObject>();

        private void Awake()
        {
            if (_levelsData.playerLevel == 1)
            {
                for (int i = 0; i < _allShops.Count; i++)
                {
                    _allShops[i].SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            InitPricePower();
            InitPriceProfit();
        }

        private void InitPricePower()
        {
            _priceText.text = _shopSettings.GetPriceToBullet().ToString();
            _numberLevel.text = "Lv." + _weaponSettings.NumberLevelBullet();

            if (_shopSettings.IsUpgradeBullet())
            {
                _btnUpgrade.interactable = true;
                _btnUpgrade.onClick.RemoveAllListeners();
                _btnUpgrade.onClick.AddListener(BuyBulletPower);
            }
            else
            {
                _btnUpgrade.interactable = false;
            }
        }

        private void BuyBulletPower()
        {
            _shopSettings.UpgradeBullet();
            _weaponSettings.UpgradeDamage();
            
            InitPricePower();
            InitPriceProfit();
        }
        
        private void InitPriceProfit()
        {
            _priceTextProfit.text = _shopSettings.GetPriceToBulletProfit().ToString();
            _numberLevelProfit.text = "Lv." + _weaponSettings.NumberLevelProfit();

            if (_shopSettings.IsUpgradeBulletProfit())
            {
                _btnUpgradeProfit.interactable = true;
                _btnUpgradeProfit.onClick.RemoveAllListeners();
                _btnUpgradeProfit.onClick.AddListener(BuyBulletProfit);
            }
            else
            {
                _btnUpgradeProfit.interactable = false;
            }
        }
        
        private void BuyBulletProfit()
        {
            _shopSettings.UpgradeBulletProfit();
            _weaponSettings.UpgradeProfit();
            
            InitPricePower();
            InitPriceProfit();
        }
    }
}