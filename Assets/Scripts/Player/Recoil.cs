using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Recoil : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _speed;
        
        private float _tmp;

        private Recoil _recoil;

        private void Awake()
        {
            _recoil = GetComponent<Recoil>();
            _recoil.enabled = false;
        }

        private void OnEnable()
        {
            _tmp = 0;
        }

        private void Update()
        {
            Rotation();
        }

        private void Rotation()
        {
            if (_tmp < _offset)
            {
                _tmp += _speed * Time.deltaTime;
                //Vector3 to = new Vector3(-_offset + transform.localEulerAngles.x, 0, 0);
                //transform.localEulerAngles = Vector3.Lerp(transform.localRotation.eulerAngles, to, _tmp);
                transform.localEulerAngles -= new Vector3(_tmp, 0, 0);
            }
            else
            {
                _recoil.enabled = false;
            }
        }
    }
}