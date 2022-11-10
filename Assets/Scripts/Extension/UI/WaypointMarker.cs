using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extension
{
    public class WaypointMarker : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Image _currentImage;
        [SerializeField] private SkinnedMeshRenderer _targetRenderer;

        private Camera _mainCamera;
        private float _increaseOnPointer;

        private float _minUIPositionX;
        private float _minUIPositionY;
        private float _maxUIPositionX;
        private float _maxUIPositionY;

        private void Awake()
        {
            _mainCamera = Camera.main;
            //_targetRenderer = _target.gameObject.GetComponent<Renderer>();

            _minUIPositionX = 100;
            _maxUIPositionX = Screen.width - _minUIPositionX;
            _minUIPositionY = 100;
            _maxUIPositionY = Screen.height - _minUIPositionY;
        }

        private void LateUpdate()
        {
            PointAtTheEnemy();
        }

        private void PointAtTheEnemy()
        {
            if (_targetRenderer.isVisible)
            {
                _currentImage.gameObject.SetActive(false);
                //Vector3 newPosition = _target.position + _offset;
                //transform.position = Camera.main.WorldToScreenPoint(newPosition);
                //_pointer.localEulerAngles = Vector3.zero;
                //_pointerText.localEulerAngles = Vector3.zero;
            }
            else
            {
                _currentImage.gameObject.SetActive(true);
                
                Vector3 targetPos = _mainCamera.WorldToScreenPoint(_target.position);
                Vector3 position = targetPos;// _mainCamera.WorldToScreenPoint(_target.position);

                float angle = 0;
                if(position.x > Screen.width)
                {
                    position.x = _maxUIPositionX;
                    angle = 90;
                }
                if(position.x < 0)
                {
                    position.x = _minUIPositionX;
                    angle = -90;
                }

                if(position.y > Screen.height)
                {
                    position.y = _maxUIPositionY;
                    angle = -180;
                }
                if(position.y < 0)
                {
                    position.y = _minUIPositionY;
                    angle = 180;
                }

                position.z = 0;
                _currentImage.rectTransform.position = position;

                _currentImage.rectTransform.localEulerAngles = new Vector3(0, 0, -angle);
            }
        }

        public void DisableMarker()
        {
            GetComponent<WaypointMarker>().enabled = false;
            _canvas.SetActive(false);
        }
    }
}