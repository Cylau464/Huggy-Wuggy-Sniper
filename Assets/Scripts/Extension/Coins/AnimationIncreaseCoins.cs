using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Extension
{
    public class AnimationIncreaseCoins : MonoBehaviour
    {
        [SerializeField] private TMP_Text _targetUI;
        
        private int _target;

        private AnimationIncreaseCoins _animationIncrease;

        private int _speed = 25;
        private int _tmp;
        
        // private void Awake()
        // {
        //     _animationIncrease = GetComponent<AnimationIncreaseCoins>();
        //     _animationIncrease.enabled = false;
        // }

        private void OnEnable()
        {
            _tmp = 0;
        }

        private void Update()
        {
            if (_target != 0)
            {
                Increase();
            }
        }

        public void SetTarget(int target)
        {
            _target = target;
        }
        
        private void Increase()
        {
            if (_tmp >= _target)
            {
                _tmp = _target;
                _targetUI.text = _tmp.ToString();
                
                GetComponent<AnimationIncreaseCoins>().enabled = false;
            }
            else
            {
                _tmp += _speed;
                _targetUI.text = _tmp.ToString();
            }
        }
    }
}