using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using UnityEngine;

namespace Human
{
    public class HumanMover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
        [SerializeField] private SmoothTurn _smoothTurn;

        private float _minDistance = 1.5f;

        private HumanMover m_humanMover;
        private AnimationCharacter _animationCharacter;

        private void Awake()
        {
            _animationCharacter = GetComponent<AnimationCharacter>();
            m_humanMover = GetComponent<HumanMover>();
            
            if (_target != null)
            {
                _target.parent = transform.parent;
            }
        }

        private void OnEnable()
        {
            _animationCharacter.OnRun();
            _smoothTurn.enabled = true;
        }

        private void Update()
        {
            if (_target != null)
            {
                Move();
            }
            else
            {
                StopRun();
            }
        }

        private void Move()
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance > _minDistance)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                direction.y = 0;
                transform.position += direction * Time.deltaTime * _speed;
            }
            else
            {
               StopRun();
            }
        }

        private void StopRun()
        {
            m_humanMover.enabled = false;
            _animationCharacter.OnScared();
        }
    }
}