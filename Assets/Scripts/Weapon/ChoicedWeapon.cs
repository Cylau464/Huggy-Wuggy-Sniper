using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Weapon
{
    public class ChoicedWeapon : MonoBehaviour
    {
        [SerializeField] private ConfigsWeapon _configsWeapon;
        [SerializeField] private TypeWeapon _typeWeapon;
        [SerializeField] private Aim _aim;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private Recharge _recharge;

        [SerializeField] private bool _isRandomChoice;
        [SerializeField] private List<TypeWeapon> _doneWeapons = new List<TypeWeapon>();

        [SerializeField] private List<ModelWeapon> _allModels = new List<ModelWeapon>();

        private static ChoicedWeapon instance;
        public static ChoicedWeapon Instance => instance;

        private void Awake()
        {
            if (instance != null && instance != this) Destroy(gameObject);
            else instance = this;
        }

        public void Init(TypeWeapon target)
        {
            if (_isRandomChoice)
            {
                int randId = UnityEngine.Random.Range(0, _doneWeapons.Count);
                _typeWeapon = _doneWeapons[randId];
            }

            _typeWeapon = target;

            _aim.SetTypeWeapon(_typeWeapon);
            _shooting.SetTypeWeapon(_typeWeapon);
            _shooting.SetBullet(_configsWeapon.GetBullet(_typeWeapon));
            _recharge.SetTimer(_configsWeapon.GetTimerRecharge(_typeWeapon));

            for (int i = 0; i < _allModels.Count; i++)
            {
                if (_allModels[i].GetTypeWeapon() == _typeWeapon)
                {
                    _allModels[i].TurnModel(true);
                }
                else
                {
                    _allModels[i].TurnModel(false);
                }
            }
        }
    }

    [Serializable]
    public class ModelWeapon
    {
        [SerializeField] private TypeWeapon _typeWeapon;
        [SerializeField] private GameObject _model;

        public TypeWeapon GetTypeWeapon()
        {
            return _typeWeapon;
        }

        public void TurnModel(bool isTurn)
        {
            _model.SetActive(isTurn);
        }
    }
}