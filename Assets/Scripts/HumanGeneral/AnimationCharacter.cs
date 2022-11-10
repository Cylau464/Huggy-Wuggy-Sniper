using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class AnimationCharacter : MonoBehaviour
    {
        [SerializeField] private string anim_idle;
        [SerializeField] private string anim_walking;
        [SerializeField] private string anim_attack;
        [SerializeField] private string anim_screame;
        [SerializeField] private string anim_scared;
        [SerializeField] private string anim_run;
        
        [SerializeField] private Animator _animator;

        [SerializeField] private bool _isEnemy = false;
        
        private void Awake()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            if (_isEnemy)
            {
                SetSpeedAnimator(0.3f);
            }
        }

        public void OnRun()
        {
            _animator.SetTrigger(anim_run);
        }
        
        public void OnWalking()
        {
            _animator.SetTrigger(anim_walking);
        }

        public void OnIdle()
        {
            _animator.SetTrigger(anim_idle);
        }

        public void OnAttack()
        {
            _animator.SetTrigger(anim_attack);
        }

        public void OnScreame()
        {
            _animator.SetTrigger(anim_screame);
        }

        public void OnScared()
        {
            _animator.SetTrigger(anim_scared);
        }

        public void SetSpeedAnimator(float speed)
        {
            _animator.speed = speed;
        }
    }
}