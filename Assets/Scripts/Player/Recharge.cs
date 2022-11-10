using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Recharge : MonoBehaviour
    {
        [SerializeField] private float _timerRecharge;

        [SerializeField] private bool _isRecharge;

        public void StartRecharge()
        {
            StopAllCoroutines();
            _isRecharge = true;
        }
        
        public void OnRecharge()
        {
            StartCoroutine(RemoveRecharge());
        }

        IEnumerator RemoveRecharge()
        {
            yield return new WaitForSeconds(_timerRecharge);

            _isRecharge = false;
        }

        public bool IsRecharge()
        {
            return _isRecharge;
        }

        public void SetTimer(float timer)
        {
            _timerRecharge = timer;
        }
    }
}