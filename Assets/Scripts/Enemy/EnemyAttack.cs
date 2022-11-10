using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using Human;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private AnimationCharacter _animationCharacter;
        [SerializeField] private EnemyMover _enemyMover;
        
        private HumanProgress target;

        private List<HumanProgress> _allHumans = new List<HumanProgress>();

        private bool _isAttack;

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.TryGetComponent(out HumanProgress humanProgress))
        //     {
        //         AttackToHuman(humanProgress);
        //     }
        // }

        public void AttackToHuman(HumanProgress humanProgress)
        {
            if (_isAttack == false)
            {
                _isAttack = true;
                _animationCharacter.OnAttack();
                _enemyMover.enabled = false;
            }

            if (_allHumans.Contains(humanProgress) == false)
            {
                _allHumans.Add(humanProgress);
            }
        }
        
        public void Attack()
        {
            _isAttack = false;

            for (int i = 0; i < _allHumans.Count; i++)
            {
                _allHumans[i].Die();
                _enemyMover.RemoveHuman(_allHumans[i].transform);
            }
            
            _allHumans.Clear();
            _enemyMover.enabled = true;
        }

        public void StartAttack()
        {
            _enemyMover.enabled = true;
            _animationCharacter.OnWalking();
        }
    }
}