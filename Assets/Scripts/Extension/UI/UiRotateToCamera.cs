using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class UiRotateToCamera : MonoBehaviour
    {
        [SerializeField] private bool _isTransformRotate = false;
        
        private Transform _cam;

        private void Awake()
        {
            _cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            RotateToCamera();
        }

        private void RotateToCamera()
        {
            transform.LookAt(_cam);

            if (_isTransformRotate)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}