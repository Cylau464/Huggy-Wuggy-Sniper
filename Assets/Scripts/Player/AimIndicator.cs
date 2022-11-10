using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AimIndicator : MonoBehaviour
    {
        [SerializeField] private Transform _model;

        private void Start()
        {
            TurnShow(false);
        }

        public void UpdatePosition(Vector3 target)
        {
            _model.position = target;
        }

        public void TurnShow(bool isTurn)
        {
            _model.gameObject.SetActive(isTurn);
        }
    }
}