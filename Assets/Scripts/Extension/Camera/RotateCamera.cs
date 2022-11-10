using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float _tmp;

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
            if (_tmp < 1)
            {
                _tmp += _speed * Time.deltaTime;
                Vector3 to = new Vector3(0, 90, 0);
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, _tmp);
            }
            else
            {
                transform.localEulerAngles = Vector3.zero;
                GetComponent<RotateCamera>().enabled = false;
            }
        }
    }
}