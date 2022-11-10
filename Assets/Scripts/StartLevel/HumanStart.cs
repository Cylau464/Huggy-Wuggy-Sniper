using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using UnityEngine;

namespace Human
{
    public class HumanStart : MonoBehaviour
    {
        [SerializeField] private HumanMover _humanMover;
        [SerializeField] private AnimationCharacter _animationCharacter;
        [SerializeField] private SmoothTurn _smoothTurn;

        private void Start()
        {
            _humanMover.enabled = false;
            Init();
        }

        private void Init()
        {
            CinemaCamera.Instance.AddHuman(GetComponent<HumanStart>());
        }

        public void OnPanic()
        {
            _animationCharacter.OnScreame();
        }

        public void OffPanic()
        {
            _humanMover.enabled = true;
            _smoothTurn.enabled = true;
        }
    }
}