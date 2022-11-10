using System;
using System.Collections;
using System.Collections.Generic;
using Bullet;
using RayFire;
using UnityEngine;

namespace Extension
{
    public class House : MonoBehaviour
    {
        [SerializeField] private Outline _outline;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BulletAttack bulletAttack))
            {
                bulletAttack.Hide();
            }
        }
        
        public void TurnOutline(bool isTurn)
        {
            if (_outline != null)
            {
                _outline.enabled = isTurn;
            }
        }
    }
}

/*                     !!! RAYFIRE !!!

//private RayfireRigid _rayfireRigid;

        // private void Awake()
        // {
        //     _rayfireRigid = GetComponent<RayfireRigid>();
        //     _rayfireRigid.Initialize();
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BulletAttack bulletAttack))
            {
                bulletAttack.Hide();
                //_rayfireRigid.Demolish();
                //Explosion(bulletAttack.transform.position);
            }
            
            // if (other.gameObject.TryGetComponent(out PartBody part))
            // {
            //     _rayfireRigid.Demolish();
            //     Explosion(part.transform.position);
            // }
            
        }

        private void Explosion(Vector3 bullet)
        {
            float radius = 5f;
            float power = 150f;
            
            //Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(bullet, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, bullet, radius, 3.0F);
            }
        }

*/