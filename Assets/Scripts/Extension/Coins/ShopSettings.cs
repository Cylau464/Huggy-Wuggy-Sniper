using System.Collections;
using System.Collections.Generic;
using engine.coin;
using UnityEngine;
using Weapon;

[CreateAssetMenu]
public class ShopSettings : ScriptableObject
{
    [SerializeField] private WeaponSettings _weaponSettings;
    [SerializeField] private CoinsData _coinsData;
    
    [Header("Насколько увеличивать ценник?")] 
    [SerializeField] private float _increaseCount;

    private string _bulletName = "LevelBullet";
    
    private string _bulletProfitName = "profit";

    private int _firstPrice = 100;

    public bool IsUpgradeBullet()
    {
        int price = GetPriceToBullet();

        if (_coinsData.IsEnoughCoins(price))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeBullet()
    {
        int price = GetPriceToBullet();
        _coinsData.RemoveCoins(price);
    }

    public int GetPriceToBullet()
    {
        if (_weaponSettings.NumberLevelBullet() == 1)
        {
            return _firstPrice;
        }
        else
        {
            float target = _firstPrice;
            for (int i = 1; i < _weaponSettings.NumberLevelBullet(); i++)
            {
                target += target / 2f;
            }

            return Mathf.FloorToInt(target);
        }
    }

    public bool IsUpgradeBulletProfit()
    {
        int price = GetPriceToBulletProfit();

        if (_coinsData.IsEnoughCoins(price))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void UpgradeBulletProfit()
    {
        int price = GetPriceToBulletProfit();
        _coinsData.RemoveCoins(price);
    }
    
    public int GetPriceToBulletProfit()
    {
        //int price = _increaseCount * (_weaponSettings.GetLvlProfit() + 1);
        //Debug.LogError(PlayerPrefs.GetInt(_bulletProfitName) + "          " + _increaseCount);
        //return price;
        if (_weaponSettings.GetLvlProfit() == 1)
        {
            //Debug.LogError("LOL");
            return _firstPrice;
        }
        else
        {
            float target = _firstPrice;
            for (int i = 1; i < _weaponSettings.GetLvlProfit(); i++)
            {
                target += target / 2f;
            }

            return Mathf.FloorToInt(target);
        }
    }
}
