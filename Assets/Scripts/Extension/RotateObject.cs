using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _direction;

        private void Update()
        {
            transform.localEulerAngles += _direction * _speed * Time.deltaTime;
        }
    }
}