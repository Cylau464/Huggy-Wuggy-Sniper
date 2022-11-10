using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class SmoothTurn : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private Transform _target;

        [SerializeField] private bool _isBlockX = false;

        [SerializeField] private Transform _model;
        
        private float _tmp;

        private SmoothTurn _smoothTurn;

        private Vector3 _targetDirection;

        private void Awake()
        {
            _smoothTurn = GetComponent<SmoothTurn>();
            //_smoothTurn.enabled = false;
        }

        private void Start()
        {
            if (_model == null)
            {
                _model = transform;
            }
        }

        private void OnEnable()
        {
            _tmp = 0;
        }

        private void LateUpdate()
        {
            if (_isBlockX)
            {
                Vector3 target = new Vector3(_target.position.x, transform.position.y, _target.position.z);
                var look = Quaternion.LookRotation(target - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, look, _tmp * Time.deltaTime);
            }
            else
            {
                var look = Quaternion.LookRotation(_target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, look, _tmp * Time.deltaTime);
            }
            
            _tmp += Time.deltaTime * _speed;
        }

        private void DisableTurn()
        {
            _smoothTurn.enabled = false;
            _tmp = 0;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            _targetDirection = _target.position;

            if (_smoothTurn == null)
            {
                _smoothTurn = GetComponent<SmoothTurn>();
            }
            _smoothTurn.enabled = true;
        }
    }
}