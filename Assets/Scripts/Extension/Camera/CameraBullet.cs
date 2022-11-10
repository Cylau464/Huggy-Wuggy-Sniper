using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Extension
{
    public class CameraBullet : MonoBehaviour
    {
        [SerializeField] private Transform _targetFirst;
        [SerializeField] private Transform _targetSecond;
        [SerializeField] private float _speed;

        [SerializeField] private GameObject _vfx;

        private float _minDistance = 1.5f;
        private Recharge _recharge;

        private float _timer = 0.25f;
        private bool _isFirstMove = true;
        private Transform _target;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_isFirstMove)
            {
                _target = _targetFirst;
            }
            else
            {
                _target = _targetSecond;
            }
            
            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance > _minDistance)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;
            }
            else
            {
                if (_isFirstMove == false)
                {
                    StartCoroutine(WaitDeactivate());
                }
            }
        }

        public void SetRecharge(Recharge recharge)
        {
            _recharge = recharge;
        }

        public void TurnNextMove()
        {
            _vfx.SetActive(false);
            _isFirstMove = false;
            _speed *= 4f;
        }

        IEnumerator WaitDeactivate()
        {
            yield return new WaitForSeconds(_timer);
            
            _recharge.OnRecharge();
            transform.parent.gameObject.SetActive(false);
            StatusGame.Instance.TurnSlowmo(false);
        }
    }
}