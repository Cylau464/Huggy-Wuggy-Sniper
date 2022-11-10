using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletMine : MonoBehaviour
    {
        [SerializeField] private Vector3 _target;

        private float _speed = 50;
        private float _minDistance = 5f;

        private BulletMine _bulletMine;

        private Rigidbody _rb;

        private void OnEnable()
        {
            _bulletMine = GetComponent<BulletMine>();
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        private void Update()
        {
            Move();
        }

        public void SetTargetPosition(Vector3 target)
        {
            _target = target;
        }
        
        private void Move()
        {
            float distance = Vector3.Distance(transform.position, _target);

            if (distance > _minDistance)
            {
                Vector3 direction = (_target - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;
            }
            else
            {
                _rb.isKinematic = false;
                
                if (gameObject.TryGetComponent(out BulletMover bulletMover))
                {
                    bulletMover.enabled = true;
                }
                
                _bulletMine.enabled = false;
            }
        }
    }
}