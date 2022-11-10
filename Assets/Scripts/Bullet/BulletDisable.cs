using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletDisable : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BulletAttack bulletAttack))
            {
                bulletAttack.Hide();
            }
        }
    }
}