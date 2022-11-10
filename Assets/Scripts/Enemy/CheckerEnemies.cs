using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using main.ui;
using Player;
using UnityEngine;

namespace Extension
{
    public class CheckerEnemies : MonoBehaviour
    {
        [SerializeField] private Aim _aim;
        [SerializeField] private Transform _parentEnemies;

        private List<GameObject> _allEnemies = new List<GameObject>();

        private CheckerEnemies m_checkerEnemies;
        
        private static CheckerEnemies instance;
        public static CheckerEnemies Instance => instance;

        private void Awake()
        {
            instance = this;
            
            Init();
        }

        private void Init()
        {
            m_checkerEnemies = GetComponent<CheckerEnemies>();
            
            for (int i = 0; i < _parentEnemies.childCount; i++)
            {
                if (_parentEnemies.GetChild(i).gameObject.TryGetComponent(out EnemyProgress enemyProgress))
                {
                    _allEnemies.Add(_parentEnemies.GetChild(i).gameObject);
                    enemyProgress.SetCheckerEnenmies(m_checkerEnemies);

                    if (enemyProgress.gameObject.TryGetComponent(out EnemyMover enemyMover))
                    {
                        enemyMover.SetChecker(m_checkerEnemies);
                    }
                }
            }
        }

        public void DieEnemy(GameObject enemy)
        {
            if (_allEnemies.Contains(enemy))
            {
                _allEnemies.Remove(enemy);

                if (_allEnemies.Count == 0)
                {
                    StatusGame.Instance.OnWin();
                    _aim.enabled = false;
                }
            }
        }

        public void KilledAllHumans()
        {
            _aim.enabled = false;

            for (int i = 0; i < _allEnemies.Count; i++)
            {
                if (_allEnemies[i].TryGetComponent(out EnemyMover enemyMover))
                {
                    enemyMover.StopMove();
                }
            }
        }
    }
}