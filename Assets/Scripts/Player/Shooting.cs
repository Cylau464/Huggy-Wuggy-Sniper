using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using Bullet;
using BzKovSoft.ObjectSlicer.Samples;
using Extension;
using UnityEngine;
using Weapon;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _positionForBullet;
        [SerializeField] private Transform _positionForBulletToLeft;
        [SerializeField] private Transform _positionForBulletToRight;

        [SerializeField] private GameObject _modelWeapon;

        private TypeWeapon _typeWeapon;

        private Aim _aim;

        private void Awake()
        {
            _aim = GetComponent<Aim>();
        }

        public void TurnWeapon(bool isTurn)
        {
            _modelWeapon.SetActive(isTurn);
        }
        
        public void Shot(Vector3 finishPosition, bool isHitEnemy, Recharge recharge)
        {
            GameObject bullet = Instantiate(_bullet, _positionForBullet.position, _positionForBullet.rotation);

            if (bullet.TryGetComponent(out BulletMover bulletMover))
            {
                bulletMover.SetTarget(finishPosition);

                if (_typeWeapon == TypeWeapon.Mine)
                {
                    bulletMover.enabled = false;

                    Vector3 targetPos = (finishPosition + transform.position) / 2f;
                    targetPos.y += 25f;

                    bullet.AddComponent<BulletMine>().SetTargetPosition(targetPos);
                }

                if (_typeWeapon == TypeWeapon.Rocket)
                {
                    bulletMover.SetTarget(finishPosition, _aim);
                    bulletMover.DecreaseSpeed();
                    _aim.SetBulletMover(bulletMover);
                }
            }

            if (_typeWeapon == TypeWeapon.Crossbow)
            {
                if (bullet.TryGetComponent(out BulletAttack bulletAttackCenter))
                {
                    //bulletAttack.TurnSlowmo(true, recharge);
                    bulletAttackCenter.TurnSlowmo(false, recharge);
                }
                
                bullet = Instantiate(_bullet, _positionForBulletToLeft.position, _positionForBulletToRight.rotation);
                
                if (bullet.TryGetComponent(out BulletMover bulletMoverLeft))
                {
                    bulletMoverLeft.SetTarget(finishPosition);
                    bulletMoverLeft.OnMoveToSide(true);
                }
                
                if (bullet.TryGetComponent(out BulletAttack bulletAttackLeft))
                {
                    //bulletAttack.TurnSlowmo(true, recharge);
                    bulletAttackLeft.TurnSlowmo(false, recharge);
                }
                
                bullet = Instantiate(_bullet, _positionForBulletToRight.position, _positionForBulletToLeft.rotation);
                
                if (bullet.TryGetComponent(out BulletMover bulletMoverRight))
                {
                    bulletMoverRight.SetTarget(finishPosition);
                    bulletMoverRight.OnMoveToSide(false);
                }
                
                if (bullet.TryGetComponent(out BulletAttack bulletAttackRight))
                {
                    //bulletAttack.TurnSlowmo(true, recharge);
                    bulletAttackRight.TurnSlowmo(false, recharge);
                }
            }

            // if (_typeWeapon == TypeWeapon.Bulgarian)
            // {
            //     if (bullet.TryGetComponent(out BzKnife bzKnife))
            //     {
            //         bzKnife.enabled = false;
            //     }
            // }
            
            if (isHitEnemy)
            {
                if (bullet.TryGetComponent(out BulletAttack bulletAttack))
                {
                    bulletAttack.TurnSlowmo(true, recharge);
                    //bulletAttack.TurnSlowmo(false, recharge);

                    if (_typeWeapon == TypeWeapon.Bulgarian)
                    {
                        bulletAttack.TurnSlicer(true);
                    }
                }
                
                StatusGame.Instance.TurnSlowmo(true);
            }
            else
            {
                if (bullet.TryGetComponent(out BulletAttack bulletAttack))
                {
                    bulletAttack.TurnSlowmo(false, recharge);
                    bulletAttack.TurnHit(false);
                }
            }

            PoolObject.Instance.AddShotFX(_positionForBullet);
        }

        public void ShotRocket(Transform target, Recharge recharge)
        {
            bool isHouse = target.gameObject.TryGetComponent(out House house);
            
            GameObject bullet = Instantiate(_bullet, _positionForBullet.position, _positionForBullet.rotation);

            if (bullet.TryGetComponent(out BulletMover bulletMover))
            {
                bulletMover.SetTarget(target, isHouse);
                bulletMover.DecreaseSpeed();
                //_aim.SetBulletMover(bulletMover);
                
                if (bullet.TryGetComponent(out BulletAttack bulletAttack))
                {
                    bulletAttack.TurnSlowmo(true, recharge);
                    //bulletAttack.TurnSlowmo(false, recharge);
                }
                
                StatusGame.Instance.TurnSlowmo(true);
            }
        }
        
        public void SetBullet(GameObject bullet)
        {
            _bullet = bullet;
        }

        public void SetTypeWeapon(TypeWeapon target)
        {
            _typeWeapon = target;
        }
    }
}