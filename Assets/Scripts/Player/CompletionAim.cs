using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CompletionAim : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxDistance = 100f;

        [SerializeField] private float _decreaseSpeed;

        [SerializeField] private Vector2 _startTouch;
        [SerializeField] private Vector2 _lastTouch;

        [SerializeField] private float _currentSpeed;

        [SerializeField] private float _distance;

        private CompletionAim _completionAim;

        [SerializeField] private bool _isMove;

        private void Awake()
        {
            _completionAim = GetComponent<CompletionAim>();
            _completionAim.enabled = false;

            _isMove = false;
        }

        private void Update()
        {
            if (_isMove)
            {
                Move();
            }
        }

        public void StartCompletion(Vector2 startTouch, Vector2 lastTouch, float minDistance)
        {
            _startTouch = startTouch;
            _lastTouch = lastTouch;
            float distance = Vector2.Distance(_startTouch, _lastTouch);

            if (distance > minDistance)
            {
                _distance = distance;

                if (_distance > _maxDistance)
                {
                    _distance = _maxDistance;
                }

                _currentSpeed = _speed;
                _completionAim.enabled = true;

                _isMove = true;
            }
        }

        private void Move()
        {
            Vector2 finalDirection = (_lastTouch - _startTouch).normalized;

            float speed = _currentSpeed * Time.deltaTime;
            float currentSpeed = _currentSpeed - _decreaseSpeed * Time.deltaTime;
            
            if (currentSpeed > _speed / 5f)
            {
                _currentSpeed = currentSpeed;
            }
            else
            {
                _isMove = false;
                _completionAim.enabled = false;
            }

            finalDirection *= speed;
            _distance -= speed;

            if (_distance > 0)
            {
                if (_model.localRotation.eulerAngles.x - finalDirection.y >= 300
                    || _model.localRotation.eulerAngles.x - finalDirection.y <= 80f)
                {
                    _model.localRotation = Quaternion.Euler(_model.localRotation.eulerAngles.x - finalDirection.y,
                        _model.localRotation.eulerAngles.y + finalDirection.x,
                        _model.localRotation.eulerAngles.z);
                }
            }
            else
            {
                _isMove = false;
                _completionAim.enabled = false;
            }
        }
    }
}