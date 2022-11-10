using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> _allRb = new List<Rigidbody>();
        [SerializeField] private Animator _animator;

        [SerializeField] private bool _isTest = false;

        private bool _isIncreaseLayer = false;
        private float _tmp = 0;
        
        [SerializeField] private bool _isMoveSpine = false;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _spine;

        private void Start()
        {
            TurnKinematic(true);
        }

        private void Update()
        {
            if (_isTest)
            {
                _isTest = false;
                
                StartCoroutine(Test());
            }

            if (_isIncreaseLayer)
            {
                _tmp += Time.deltaTime;
                _animator.SetLayerWeight(_animator.GetLayerIndex("Layer"), _tmp);

                if (_tmp > 1)
                {
                    _isIncreaseLayer = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isMoveSpine)
            {
                _spine.GetComponent<Rigidbody>().MovePosition(-_spine.forward * _speed * Time.fixedDeltaTime);
            }
        }

        private void TurnKinematic(bool isTurn)
        {
            for (int i = 0; i < _allRb.Count; i++)
            {
                _allRb[i].isKinematic = isTurn;
            }
        }

        private void TurnColliderTrigger(bool isTurn)
        {
            for (int i = 0; i < _allRb.Count; i++)
            {
                if (_allRb[i].gameObject.TryGetComponent(out Collider collider))
                {
                    collider.isTrigger = isTurn;
                }
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);
            
            TurnKinematic(false);
            _animator.enabled = false;
            TurnColliderTrigger(false);
            
            _animator.SetLayerWeight(_animator.GetLayerIndex("Layer"), 0);
            
            yield return new WaitForSeconds(2.5f);
            
            TurnKinematic(true);
            _animator.enabled = true;
            TurnColliderTrigger(true);
            
            // yield return new WaitForSeconds(2.5f);
            //
            // _isIncreaseLayer = true;
            // _tmp = 0;
            // TurnKinematic(true);
            // _animator.ApplyBuiltinRootMotion();
            // _animator.enabled = true;
            // _animator.SetTrigger("Scream");
            // _animator.CrossFade("Scream",0.3f);
            // TurnColliderTrigger(true);
        }
    }
}