using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class LaserAttack : MonoBehaviour
    {
        [SerializeField] private WeaponSettings _weaponSettings;

        private float _damage;

        private void Awake()
        {
            _damage = _weaponSettings.GetDamage(true);
        }

        public float GetCountDamage()
        {
            return _damage;
        }
    }
}