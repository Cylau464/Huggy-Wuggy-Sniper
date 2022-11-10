using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Extension
{
    public class WheelWeapons : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _decreaseSpeed;

        [SerializeField] private ArrowWheelWeapons _arrowWheelWeapons;

        private float _currentSpeed;

        private WheelWeapons _wheelWeapons;

        private void Awake()
        {
            _wheelWeapons = GetComponent<WheelWeapons>();
        }

        private void OnEnable()
        {
            int randSpeed = UnityEngine.Random.Range((int)_startSpeed, (int) _startSpeed * 2);
            _currentSpeed = randSpeed;
        }

        private void Update()
        {
            RotateWheel();
        }

        private void RotateWheel()
        {
            if (_currentSpeed > 0)
            {
                transform.localEulerAngles += Vector3.forward * _currentSpeed * Time.deltaTime;

                _currentSpeed -= _decreaseSpeed * Time.deltaTime;
            }
            else
            {
                if (_arrowWheelWeapons.IsTriggerAnyObj())
                {
                    SetWeapon(_arrowWheelWeapons.GetTextInWheel());
                    _wheelWeapons.enabled = false;
                }
                else
                {
                    transform.localEulerAngles += Vector3.forward * _minSpeed * Time.deltaTime;
                }
            }
        }

        private void SetWeapon(string text)
        {
            switch (text)
            {
                case "1":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Rocket);
                    break;
                
                case "2":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Mine);
                    break;
                
                case "3":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Bulgarian);
                    break;
                
                case "4":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Minigun);
                    break;
                
                case "5":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Sniper);
                    break;
                
                case "6":
                    Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Crossbow);
                    break;
            }

            transform.parent.gameObject.SetActive(false);
        }
    }
}