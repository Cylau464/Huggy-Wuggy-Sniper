using System;
using System.Collections;
using System.Collections.Generic;
using Bullet;
using Extension;
using RayFire;
using RootMotion.Dynamics;
using UnityEngine;

namespace Enemy
{
    public class EnemyProgress : MonoBehaviour
    {
        [SerializeField] private RayfireRigid _rayfireRigid;
        [SerializeField] private PuppetMaster _puppetMaster;
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private GameObject _bloodFX;
        [SerializeField] private Outline _outline;
        
        private StatusHP _statusHp;
        private CheckerEnemies _checkerEnemies;

        private bool _isDead = false;

        private float _timerForRagdoll = 3f;
        private bool _isDeath = false;
        private float _tmp = 0;

        private float _timerAttack = 0.25f;

        private bool _isTakeDamage;
        
        private void Awake()
        {
            _statusHp = GetComponent<StatusHP>();
            _rayfireRigid.Initialize();

            _isTakeDamage = true;
        }

        private void Update()
        {
            if (_isDeath)
            {
                _tmp += Time.deltaTime;
                if (_tmp > _timerForRagdoll)
                {
                    _isDeath = false;
                    _puppetMaster.Resurrect();
                    _enemyMover.enabled = true;
                }
            }
        }

        public void SetCheckerEnenmies(CheckerEnemies checkerEnemies)
        {
            _checkerEnemies = checkerEnemies;
        }

        public void TakeDamage(float count)
        {
            StartCoroutine(WaitActivateTakeDamage());
            
            _statusHp.TakeDamage(count);

            if (_statusHp.IsDead())
            {
                Die();
            }
            else
            {
                if (count > 0)
                {
                    WaitRagdoll();
                }
            }
        }

        public void CreateBlood(Vector3 target)
        {
            GameObject blood = Instantiate(_bloodFX, target, Quaternion.identity);
            blood.transform.LookAt(transform);
            blood.transform.Rotate(0, 180, 0);
        }

        private  void WaitRagdoll()
        {
            _enemyMover.enabled = false;
            _puppetMaster.Kill();
            _tmp = 0;
            _isDeath = true;
        }

        public void Die()
        {
            if (_isDead == false)
            {
                _isDead = true;

                GetComponent<WaypointMarker>().DisableMarker();

                _checkerEnemies.DieEnemy(gameObject);

                _rayfireRigid.Demolish();

                gameObject.SetActive(false);
            }
        }

        IEnumerator WaitActivateTakeDamage()
        {
            _isTakeDamage = false;
            
            yield return new WaitForSeconds(_timerAttack);

            _isTakeDamage = true;
        }

        public bool IsTakeDamage()
        {
            return _isTakeDamage;
        }

        public void TurnOutline(bool isTurn)
        {
            if (_outline != null)
            {
                _outline.enabled = isTurn;
            }
        }
    }
}