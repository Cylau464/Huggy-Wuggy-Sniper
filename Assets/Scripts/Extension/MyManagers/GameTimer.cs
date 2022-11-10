using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Extension
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float _timer;
        [SerializeField] private TMP_Text _text;

        private GameTimer _gameTimer;

        private bool _isAgreeActivate = true;

        private static GameTimer instance;
        public static GameTimer Instance => instance;
        
        private void Awake()
        {
            instance = this;
            
            _gameTimer = GetComponent<GameTimer>();
            _gameTimer.enabled = false;
            
            _text.SetText(((int)_timer).ToString());
        }

        private void Update()
        {
            DecreaseTimer();
        }

        private void DecreaseTimer()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = 0;
                DisableTimer();
            }
            
            _text.SetText(((int)_timer).ToString());
        }

        public void EnableTimer()
        {
            _gameTimer.enabled = true;
        }
        
        private void DisableTimer()
        {
            if (_isAgreeActivate)
            {
                _text.SetText("0");
                _gameTimer.enabled = false;
                CheckerEnemies.Instance.KilledAllHumans();
                StatusGame.Instance.OnLose();
            }
        }

        public void OffTimer()
        {
            _isAgreeActivate = false;
        }
    }
}