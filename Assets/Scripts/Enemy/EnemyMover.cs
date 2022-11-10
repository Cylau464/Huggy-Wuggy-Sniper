using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using Human;
using UnityEngine;
using Random = System.Random;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Transform _parentHumans;
        [SerializeField] private EnemyAttack _enemyAttack;
        
        private List<Transform> _allHumans = new List<Transform>();

        [SerializeField] private Transform _target;

        [SerializeField] private SmoothTurn _smoothTurn;

        private float _minDistance = 10f;

        private EnemyMover _enemyMover;

        private CheckerEnemies _checkerEnemies;

        private void Awake()
        {
            _enemyMover = GetComponent<EnemyMover>();
            
            for (int i = 0; i < _parentHumans.childCount; i++)
            {
                if(_parentHumans.GetChild(i).gameObject.TryGetComponent(out HumanMover humanMover))
                {
                    _allHumans.Add(_parentHumans.GetChild(i));
                }
            }
            
            SetTarget();
        }
        
        private void Update()
        {
            Move();
        }

        public void SetChecker(CheckerEnemies checkerEnemies)
        {
            _checkerEnemies = checkerEnemies;
        }

        private void SetTarget()
        {
            if (_allHumans.Count > 0)
            {
                int randId = UnityEngine.Random.Range(0, _allHumans.Count);
                _target = _allHumans[randId];
                _smoothTurn.SetTarget(_target);
            }
            else
            {
                _enemyMover.enabled = false;
                _smoothTurn.enabled = false;
            }
        }

        public void RemoveHuman(Transform human)
        {
            if (_allHumans.Contains(human))
            {
                _allHumans.Remove(human);

                if (_allHumans.Count > 0)
                {
                    SetTarget();
                }
                else
                {
                    StatusGame.Instance.OnLose();
                    _checkerEnemies.KilledAllHumans();
                }
            }
        }

        public void StopMove()
        {
            _enemyMover.enabled = false;

            if (_enemyAttack.gameObject.TryGetComponent(out AnimationCharacter animationCharacter))
            {
                animationCharacter.OnIdle();
            }
        }
        
        private void Move()
        {
            if (_target != null)
            {
                Vector3 target = _target.position;
                target.y = transform.position.y;
                float distance = Vector3.Distance(transform.position, target);

                if (distance > _minDistance)
                {
                    Vector3 direction = (_target.position - transform.position).normalized;
                    direction.y = 0;
                    transform.position += direction * _speed * Time.deltaTime;
                }
                else
                {
                    if (_target.gameObject.activeInHierarchy == false)
                    {
                        SetTarget();
                    }
                    else
                    {
                        _enemyMover.enabled = false;

                        if (_target.TryGetComponent(out HumanProgress humanProgress))
                        {
                            _enemyAttack.AttackToHuman(humanProgress);
                        }
                    }
                }
            }
        }
    }
}