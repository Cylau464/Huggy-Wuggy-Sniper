using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using MPUIKIT;
using Player;
using UnityEngine;

namespace Weapon
{
    public class AimRocket : MonoBehaviour
    {
        [SerializeField] private Aim _aim;
        [SerializeField] private float _timer;
        [SerializeField] private MPImage _mpImage;
        
        private float _currentTimer;
        
        private PartBody _lastPartBody;
        private House _lastHouse;
        
        private Camera _cam;

        private Vector3 _offsetForHouse = new Vector3(0, 5, 0);

        private void Awake()
        {
            _cam = Camera.main;
            UpdateUI(false);
        }

        public void CaptureAim(PartBody partBody)
        {
            if (partBody != null)
            {
                if (_lastHouse != null)
                {
                    _lastHouse.TurnOutline(false);
                    ResetTimer();
                    _lastHouse = null;
                }
                
                _lastPartBody = partBody;
                _lastPartBody.TurnOutline(true);
                DecreaseTimer();
            }
            else
            {
                if (_lastPartBody != null)
                {
                    _lastPartBody.TurnOutline(false);
                }

                _lastPartBody = null;
                ResetTimer();
            }
        }

        public void CaptureAim(House house)
        {
            if (house != null)
            {
                if (_lastHouse != house && _lastHouse != null)
                {
                    _lastHouse.TurnOutline(false);
                    ResetTimer();
                }
                
                if (_lastPartBody != null)
                {
                    _lastPartBody.TurnOutline(false);
                    ResetTimer();
                    _lastPartBody = null;
                }
                
                _lastHouse = house;
                _lastHouse.TurnOutline(true);
                DecreaseTimer();
            }
            else
            {
                if (_lastHouse != null)
                {
                    _lastHouse.TurnOutline(false);
                }

                _lastHouse = null;
                ResetTimer();
            }
        }

        public void StopCapture()
        {
            if (_lastHouse != null)
            {
                _lastHouse.TurnOutline(false);
                _lastHouse = null;
            }

            if (_lastPartBody != null)
            {
                _lastPartBody.TurnOutline(false);
                _lastPartBody = null;
            }

            ResetTimer();
        }
        
        private void DecreaseTimer()
        {
            _currentTimer -= Time.deltaTime;

            UpdateUI(true);
            
            if (_currentTimer < 0)
            {
                if (_lastHouse != null)
                {
                    _aim.ShotRocket(_lastHouse.transform);
                }
                else
                {
                    _aim.ShotRocket(_lastPartBody.transform);
                }

                ResetTimer();
            }
        }

        private void UpdateUI(bool isShow)
        {
            if (isShow)
            {
                if (_mpImage.gameObject.activeInHierarchy == false)
                {
                    _mpImage.gameObject.SetActive(true);
                }
            }
            else
            {
                if (_mpImage.gameObject.activeInHierarchy)
                {
                    _mpImage.gameObject.SetActive(false);
                }
            }
            
            _mpImage.fillAmount = _currentTimer / _timer;

            if (_lastPartBody != null)
            {
                _mpImage.transform.position = _cam.WorldToScreenPoint(_lastPartBody.transform.position);
            }

            if (_lastHouse != null)
            {
                _mpImage.transform.position = 
                    _cam.WorldToScreenPoint(_lastHouse.transform.position + _offsetForHouse);
            }
        }

        public void ResetTimer()
        {
            _currentTimer = _timer;
            UpdateUI(false);
        }
    }
}