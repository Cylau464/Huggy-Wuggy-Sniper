using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extension
{
    public class ArrowWheelWeapons : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Text text))
            {
                _text = text;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Text text))
            {
                _text = null;
            }
        }

        public bool IsTriggerAnyObj()
        {
            return _text != null;
        }

        public string GetTextInWheel()
        {
            return _text.text;
        }
    }
}