using System.Collections;
using System.Collections.Generic;
using main.level;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu]
    public class WeaponSettings : ScriptableObject
    {
        [Header("Урон от пули")] [SerializeField]
        private float _damage;

        [Header("Насколько увеличивать урон")] [SerializeField]
        private float _upgradeDamage;

        [Header("Во сколько уменьшать урон лазеру?")] [SerializeField]
        private float _decreaseDamageToLaser;
        
        private string _bulletName = "LevelBullet";
        
        //profit

        private string _bulletProfitName = "profit";

        //[SerializeField] private int _lvlProfit = 0;

        public float GetDamage(bool isLaser)
        {
            if (NumberLevelBullet() == 1)
            {
                if (isLaser)
                {
                    return _damage / _decreaseDamageToLaser;
                }
                else
                {
                    return _damage;
                }
            }
            else
            {
                float target = _damage;
                for (int i = 1; i < NumberLevelBullet(); i++)
                {
                    target += _upgradeDamage;
                }

                if (isLaser)
                {
                    target = target / _decreaseDamageToLaser;
                }
                
                return target;
            }
        }

        public void UpgradeDamage()
        {
            int tmp = PlayerPrefs.GetInt(_bulletName);
            PlayerPrefs.SetInt(_bulletName, tmp + 1);
        }

        public void UpgradeProfit()
        {
            //_lvlProfit++;
            
            int tmp = PlayerPrefs.GetInt(_bulletProfitName);
            PlayerPrefs.SetInt(_bulletProfitName, tmp + 1);
        }

        public int NumberLevelBullet()
        {
            return PlayerPrefs.GetInt(_bulletName) + 1;
        }

        public int NumberLevelProfit()
        {
            return PlayerPrefs.GetInt(_bulletProfitName) + 1;
        }

        public int GetLvlProfit()
        {
            //Debug.LogError(PlayerPrefs.GetInt(_bulletProfitName));
            return PlayerPrefs.GetInt(_bulletProfitName) + 1; //_lvlProfit + 1;
        }
    }
}