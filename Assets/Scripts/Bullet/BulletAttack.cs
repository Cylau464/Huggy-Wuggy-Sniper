using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using Player;
using UnityEngine;
using Weapon;

namespace Bullet
{
    public class BulletAttack : MonoBehaviour
    {
        [SerializeField] private WeaponSettings _weaponSettings;
        [SerializeField] private Transform _camSettings;
        [SerializeField] private CameraBullet _cameraBullet;

        private bool _isHit = true;

        private bool _isSlicer = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BackForRaycast"))
            {
                Hide();
            }
        }

        public void TurnSlowmo(bool isTurn, Recharge recharge)
        {
            _camSettings.gameObject.SetActive(isTurn);
            _cameraBullet.SetRecharge(recharge);
        }

        public void TurnHit(bool isTurn)
        {
            _isHit = isTurn;
        }
        
        public float GetCountDamage()
        {
            if (_isHit)
            {
                return _weaponSettings.GetDamage(false);
            }
            else
            {
                return 0;
            }
        }

        public void Hide()
        {
            if (_camSettings.gameObject.activeInHierarchy)
            {
                _camSettings.parent = null;
                _cameraBullet.enabled = true;
                _cameraBullet.TurnNextMove();
            }

            PoolObject.Instance.AddExplosionFX(transform.position);
            gameObject.SetActive(false);
        }

        public void TurnSlicer(bool isTurn)
        {
            _isSlicer = isTurn;
        }
        
        public bool IsSlicer()
        {
            return _isSlicer;
        }
    }
}