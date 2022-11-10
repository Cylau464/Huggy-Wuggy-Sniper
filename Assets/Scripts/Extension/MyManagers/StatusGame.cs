using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class StatusGame : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private float _timeSlowmo;
        
        private static StatusGame instance;
        public static StatusGame Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        public void OnLose()
        {
            _gameManager.MakeFailed();
        }

        public void OnWin()
        {
            _gameManager.MakeCompleted();
        }

        public void TurnSlowmo(bool isTurn)
        {
            if (isTurn)
            {
                Time.timeScale = _timeSlowmo;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}