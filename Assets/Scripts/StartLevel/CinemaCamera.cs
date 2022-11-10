using System;
using System.Collections;
using System.Collections.Generic;
using Human;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Extension
{
    public class CinemaCamera : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _player;

        [SerializeField] private SmoothTurn _smoothTurnToEnemy;
        [SerializeField] private SmoothTurn _smoothTurnToRoseEnemy;

        [SerializeField] private Zoom _zoom;
        [SerializeField] private AnimationCharacter _animationHuggy;
        [SerializeField] private AnimationCharacter _animationHuggyRose;

        [SerializeField] private RotateCamera _smoothTurnPlayer;

        [SerializeField] private bool _isSkipCinema = false;

        [SerializeField] private List<AnimationCharacter> _allEnemies = new List<AnimationCharacter>();

        private List<HumanStart> _allHumans = new List<HumanStart>();

        private bool _isMove = false;
        private float _minDistance = 1.5f;

        private static CinemaCamera instance;
        public static CinemaCamera Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     StartCinema();
            // }

            if (_isMove)
            {
                Move();
            }
        }

        public void StartCinema()
        {
            PanicHuman();

            if (_isSkipCinema)
            {
                if (_allEnemies.Count > 0)
                {
                    for (int i = 0; i < _allEnemies.Count; i++)
                    {
                        _allEnemies[i].OnScreame();
                    }
                }
                else
                {
                    _animationHuggy.OnScreame();

                    if (_animationHuggyRose != null)
                    {
                        _animationHuggyRose.OnScreame();
                    }
                }

                DisableMove();
            }
            else
            {
                StartCoroutine(WaitSmoothTurnToEnemy());
            }
        }

        public void AddHuman(HumanStart humanStart)
        {
            _allHumans.Add(humanStart);
        }
        
        private void PanicHuman()
        {
            for (int j = 0; j < _allHumans.Count; j++)
            {
                _allHumans[j].OnPanic();
            }
        }

        private void Move()
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance > _minDistance)
            {
                Vector3 direction = (_player.position - transform.position).normalized;
                transform.position += direction * Time.deltaTime * _speed;
            }
            else
            {
                DisableMove();
            }
        }

        private void DisableMove()
        {
            _isMove = false;
            
            _smoothTurnToEnemy.enabled = false;

            if (_smoothTurnToRoseEnemy != null)
            {
                _smoothTurnToRoseEnemy.enabled = false;
            }

            Transform mainCam = _zoom.transform;
            mainCam.parent = _player;
            mainCam.localPosition = Vector3.zero;
            mainCam.localEulerAngles = Vector3.zero;

            if (_player.gameObject.TryGetComponent(out Aim aim))
            {
                aim.enabled = true;
            }
            
            Extension.GameTimer.Instance.EnableTimer();
            
            gameObject.SetActive(false);
        }
        
        IEnumerator WaitSmoothTurnToEnemy()
        {
            yield return new WaitForSeconds(1.5f);

            _smoothTurnToEnemy.enabled = true;

            yield return new WaitForSeconds(0.5f);
            
            _zoom.SetZoom(false, false);
            _animationHuggy.OnScreame();
            
            if (_animationHuggyRose != null)
            {
                yield return new WaitForSeconds(2.5f);
                
                _smoothTurnToEnemy.enabled = false;
                _smoothTurnToRoseEnemy.enabled = true;
                _zoom.SetZoom(true, false);

                yield return new WaitForSeconds(1f);
            
                _zoom.SetZoom(false, false);
                
                _animationHuggyRose.OnScreame();
                
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(4f);
            }

            _zoom.SetZoom(true, false);

            _isMove = true;
            _smoothTurnPlayer.enabled = true;
        }

    }
}