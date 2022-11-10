using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Human
{
    public class HumanProgress : MonoBehaviour
    {
        public void Die()
        {
            gameObject.SetActive(false);
        }
    }
}