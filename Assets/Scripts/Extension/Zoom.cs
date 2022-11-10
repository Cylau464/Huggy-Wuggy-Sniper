using System;
using System.Collections;
using System.Collections.Generic;
using main.ui;
using UnityEngine;

namespace Extension
{
    public class Zoom : MonoBehaviour
    {
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;
        [SerializeField] private float _speed;

        private bool _isDecrease;

        private Camera cam;

        private Zoom m_zoom;

        private bool _isShowUI;

        private void Awake()
        {
            _isDecrease = false;
            cam = GetComponent<Camera>();
            cam.fieldOfView = _maxZoom;
            
            m_zoom = GetComponent<Zoom>();
            m_zoom.enabled = false;
        }

        private void Start()
        {
            _maxZoom = cam.fieldOfView;
            //_minZoom = _maxZoom - 15f;
        }

        private void Update()
        {
            OnZoom();
        }

        public void SetZoom(bool isDecrease, bool isShowUI)
        {
            _isDecrease = isDecrease;
            _isShowUI = isShowUI;
            m_zoom.enabled = true;
        }
        
        private void OnZoom()
        {
            if (_isDecrease)
            {
                if (cam.fieldOfView < _maxZoom)
                {
                    cam.fieldOfView += _speed * Time.deltaTime;
                }
                else
                {
                    cam.fieldOfView = _maxZoom;
                    m_zoom.enabled = false;
                }
            }
            else
            {
                if (cam.fieldOfView > _minZoom)
                {
                    cam.fieldOfView -= _speed * Time.deltaTime;
                }
                else
                {
                    cam.fieldOfView = _minZoom;
                    if (_isShowUI)
                    {
                        MainCanvasManager.Instance.ShowAim();
                    }

                    m_zoom.enabled = false;
                }
            }
        }
    }
}