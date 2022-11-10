using System;
using System.Collections;
using System.Collections.Generic;
using main.level;
using Player;
using RayFire;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Extension
{
    public class StatusHP : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Image _statusBar;
        [SerializeField] private float _hp;
        [SerializeField] private float _armor;

        [SerializeField] private bool _isPlayer;
        [SerializeField] private LevelsData _levelsData;

        [SerializeField] private float _currentHP;

        private float _lastHP;

        private bool _isDead = false;

        private void Awake()
        {
            _currentHP = _hp;
            _canvas.SetActive(false);

            if (_isPlayer == false)
            {
                _currentHP *= _levelsData.playerLevel;
            }
            
            _lastHP = _currentHP;
        }

        public void SetArmor(float armor)
        {
            _armor = armor;
        }
        
        public void TakeDamage(float count, Vector3 position = default)
        {
            if (_canvas.activeInHierarchy == false && count > 0)
            {
                _canvas.SetActive(true);
            }

            _currentHP -= count;

            if (_currentHP <= 0)
            {
                _currentHP = 0;
                _canvas.SetActive(false);
                _isDead = true;
            }

            _statusBar.fillAmount = _currentHP / _lastHP;
        }

        public bool IsFullRecovery()
        {
            return _currentHP == _hp;
        }

        public void Recovery(float count)
        {
            _currentHP += count;

            if (_currentHP > _hp)
            {
                _currentHP = _hp;
            }
            
            _statusBar.fillAmount = _currentHP / _hp;
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void TurnCanvasHP(bool isTurn)
        {
            _canvas.SetActive(isTurn);
        }
    }
}