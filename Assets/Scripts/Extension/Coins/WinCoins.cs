using System;
using System.Collections;
using System.Collections.Generic;
using engine;
using engine.coin;
using main.level;
using TMPro;
using UnityEngine;
using Weapon;

namespace Extension
{
    public class WinCoins : MonoBehaviour
    {
        [SerializeField] private CoinsData _coinsData;
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private WeaponSettings _weaponSettings;

        [Header("Со скольки начинать давать?")]
        [SerializeField] private int _totalCoin;

        [SerializeField] private int _firstExtraCoins;

        [SerializeField] private TMP_Text _task;
        [SerializeField] private TMP_Text _extra;
        [SerializeField] private TMP_Text _total;

        [SerializeField] private AnimationIncreaseCoins _animationIncreaseCoinsTask;
        [SerializeField] private AnimationIncreaseCoins _animationIncreaseCoinsExtra;
        [SerializeField] private AnimationIncreaseCoins _animationIncreaseCoinsTotal;

        [SerializeField] private TMP_Text _numberLevel;
        
        private void OnEnable()
        {
            AddAllCoins();
        }

        public void AddAllCoins()
        {
            _numberLevel.text = "Level " + (_levelsData.playerLevel - 1).ToString();
            
            int lvl = _levelsData.playerLevel;
            int total = _totalCoin * (lvl - 1);

            int increase = 0;
            if (_weaponSettings.GetLvlProfit() == 0)
            {
                increase = 1;
            }
            else
            {
                increase = _weaponSettings.GetLvlProfit();
            }
            int extra = _firstExtraCoins * increase;

            _animationIncreaseCoinsTask.SetTarget(total);
            _animationIncreaseCoinsExtra.SetTarget(extra);

            total += extra;
            _animationIncreaseCoinsTotal.SetTarget(total);
            
            _coinsData.AddCoins(total);
        }
    }
}