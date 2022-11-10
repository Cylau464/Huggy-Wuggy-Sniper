using System.Collections;
using System.Collections.Generic;
using Bullet;
using Enemy;
using UnityEngine;
using Weapon;

namespace Extension
{
    public class PartBody : MonoBehaviour
    {
        [SerializeField] private EnemyProgress _enemyProgress;

        [SerializeField] private Transform _modelPartBody;
        [SerializeField] private bool _isSlice;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BulletAttack bulletAttack))
            {
                if (_enemyProgress.IsTakeDamage())
                {
                    _enemyProgress.TakeDamage(bulletAttack.GetCountDamage());
                    _enemyProgress.CreateBlood(bulletAttack.transform.position);

                    if (bulletAttack.IsSlicer())
                    {
                        Slice();
                    }
                    
                    bulletAttack.Hide();
                }
            }

            if (other.gameObject.TryGetComponent(out LaserAttack laserAttack))
            {
                _enemyProgress.TakeDamage(laserAttack.GetCountDamage());
                _enemyProgress.CreateBlood(transform.position);
                Slice();
            }
        }

        public void TurnOutline(bool isTurn)
        {
            _enemyProgress.TurnOutline(isTurn);
        }

        private void Slice()
        {
            if (_isSlice && _modelPartBody != null)
            {
                _modelPartBody.localScale = Vector3.zero;
            }
        }
    }
}