using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using TMPro;

namespace Extension
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionFX;
        [SerializeField] private GameObject _shotFX;
        
        private ManagerPool _managerPool = new ManagerPool();
        private List<GameObject> _allFx = new List<GameObject>();

        private static PoolObject instance;
        public static PoolObject Instance => instance;

        private void Awake()
        {
            Init();

            _managerPool.AddPool(PoolType.Fx);
        }

        private void Init()
        {
            if (instance != null && instance != this) Destroy(gameObject);
            else instance = this;
        }

        public void AddExplosionFX(Vector3 target)
        {
            _allFx.Add(_managerPool.Spawn(PoolType.Fx, _explosionFX, target, Quaternion.identity));
        }

        public void AddShotFX(Transform parent)
        {
            _allFx.Add(_managerPool.Spawn(PoolType.Fx, _shotFX, Vector3.zero, Quaternion.identity, parent));
            _allFx[_allFx.Count - 1].transform.localPosition = Vector3.zero;
        }
        
        public void RemoveObject(PoolType type, GameObject obj)
        {
            _managerPool.Despawn(type, obj);
        }
    }
}