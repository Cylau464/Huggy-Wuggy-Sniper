using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Bullet
{
    public class BulletMover : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _aceleration;
        
        private Rigidbody _rb;
        private Vector3 _target;
        private Transform _targetTransform;

        private BulletMover m_bulletMover;

        private Aim _aim;

        private float _offsetToSide = 10;
        private Vector3 _offsetForHouse = new Vector3(0, 5, 0);

        private bool _isHouse = false;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            m_bulletMover = GetComponent<BulletMover>();
            m_bulletMover.enabled = false;
        }

        private void OnDisable()
        {
            if (_aim != null)
            {
                _aim.StopAiming();
            }
        }

        private void Update()
        {
            if (_targetTransform == null)
            {
                Move();
            }
            else
            {
                UpdateTarget();
            }
        }

        public void SetTarget(Vector3 target, Aim aim = default)
        {
            _target = target;
            m_bulletMover.enabled = true;

            _aim = aim;
            
            StartMove();
        }
        
        public void SetTarget(Transform target, bool isHouse, Aim aim = default)
        {
            _targetTransform = target;
            m_bulletMover.enabled = true;

            _aim = aim;
            _isHouse = isHouse;
            
            StartMove();
        }

        public void DecreaseSpeed()
        {
            _maxSpeed = 50f;
        }

        public void OnMoveToSide(bool isLeft)
        {
            if (isLeft)
            {
                _target -= transform.forward * _offsetToSide;
            }
            else
            {
                _target += -transform.forward * _offsetToSide;
            }
        }

        public void UpdateTarget()
        {
            Vector3 direction = Vector3.zero;
            
            if (_isHouse)
            {
                direction = (_offsetForHouse + _targetTransform.position) - transform.position;
            }
            else
            {
                direction = _targetTransform.position - transform.position;
            }
            
            direction.Normalize();
            _rb.velocity = direction * _maxSpeed;
        }

        private void StartMove()
        {
            _rb.isKinematic = false;

            Vector3 direction = _target - transform.position;
            direction.Normalize();
            _rb.velocity = direction * (_maxSpeed);
        }

        private void Move()
        {
            transform.forward = _rb.velocity;
            
            if (_rb.velocity.magnitude < _maxSpeed)
            {
                Vector3 direction = _target - transform.position;
                direction.Normalize();
                _rb.AddForce(direction * _aceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
    }
}