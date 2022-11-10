using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class LaserScale : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxScale;
        [SerializeField] private Vector3 _direction;

        private LaserScale _laserScale;

        private bool _isIncrease = true;

        private Vector3 _startScale;

        private void Awake()
        {
            _laserScale = GetComponent<LaserScale>();
            _laserScale.enabled = false;

            _startScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            if (_isIncrease)
            {
                transform.localScale = _startScale;
            }
        }

        private void OnDisable()
        {
            if (!_isIncrease)
            {
                transform.localScale = Vector3.zero;
            }
        }

        private void Update()
        {
            if (_isIncrease)
            {
                IncreaseScale();
            }
            else
            {
                DecreaseScale();
            }
        }

        public void TurnIncrease(bool isTurn)
        {
            _isIncrease = isTurn;
        }
        
        private void IncreaseScale()
        {
            if (transform.localScale.x < _maxScale)
            {
                transform.localScale += _direction * _speed;
            }
            else
            {
                transform.localScale = new Vector3(_maxScale, transform.localScale.y, transform.localScale.z);
                _laserScale.enabled = false;
            }
        }

        private void DecreaseScale()
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= _direction * _speed;
            }
            else
            {
                transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
                _laserScale.enabled = false;
            }
        }
    }
}