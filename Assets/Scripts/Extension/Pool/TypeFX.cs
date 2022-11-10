using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Extension
{
    public class TypeFX : MonoBehaviour
    {
        [SerializeField] private PoolType _currentType;
        
        [SerializeField] private GameObject _targetObj;

        [SerializeField] private bool _isCoroutineDeactivate = false;
        [SerializeField] private float _timer;
        
        private void OnEnable()
        {
            if (_isCoroutineDeactivate)
            {
                StartCoroutine(WaitDeactivate());
            }
        }

        private void OnParticleSystemStopped()
        {
            Hide();
        }

        private void Hide()
        {
            if (_targetObj == null)
            {
                Extension.PoolObject.Instance.RemoveObject(_currentType, gameObject);
            }
            else
            {
                Extension.PoolObject.Instance.RemoveObject(_currentType, _targetObj);
            }
        }

        IEnumerator WaitDeactivate()
        {
            yield return new WaitForSeconds(_timer);
            
            Hide();
        }
    }
}