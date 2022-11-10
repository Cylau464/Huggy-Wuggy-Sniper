using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu]
    public class ConfigsWeapon : ScriptableObject
    {
        [SerializeField] private List<ConfigWeapon> _allConfigs = new List<ConfigWeapon>();

        public float GetTimerRecharge(TypeWeapon target)
        {
            int id = GetIdConfig(target);
            
            return _allConfigs[id].GetTimerRecharge();
        }

        public GameObject GetBullet(TypeWeapon target)
        {
            int id = GetIdConfig(target);
            
            return _allConfigs[id].GetBullet();
        }
        
        private int GetIdConfig(TypeWeapon target)
        {
            int id = -1;
            
            for (int i = 0; i < _allConfigs.Count; i++)
            {
                if (_allConfigs[i].GetTypeWeapon() == target)
                {
                    id = i;
                }
            }

            if (id == -1)
            {
                Debug.LogError("NULL !!!");
            }

            return id;
        }
        
    }

    [Serializable]
    public class ConfigWeapon
    {
        [SerializeField] private TypeWeapon _typeWeapon;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _timerRecharge;

        public TypeWeapon GetTypeWeapon()
        {
            return _typeWeapon;
        }

        public float GetTimerRecharge()
        {
            return _timerRecharge;
        }

        public GameObject GetBullet()
        {
            return _bullet;
        }
    }
}