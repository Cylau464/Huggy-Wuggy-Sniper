using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Weapon
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Aim _aim;
        [SerializeField] private LaserScale _laserScale;
        [SerializeField] private float _timerAttack;

        public void StartLaser()
        {
            _laserScale.TurnIncrease(true);
            _laserScale.enabled = true;
            StartCoroutine(WaitDeactivate());
        }

        public void StopLaser()
        {
            StopAllCoroutines();
            _laserScale.TurnIncrease(false);
            _laserScale.enabled = true;
        }

        IEnumerator WaitDeactivate()
        {
            yield return new WaitForSeconds(_timerAttack);

            if (_aim.enabled)
            {
                _aim.StopAiming();
                StopLaser();
            }
        }

        public void HideLaser()
        {
            gameObject.SetActive(false);
        }
    }
}