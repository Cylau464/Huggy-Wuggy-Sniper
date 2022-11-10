using System;
using System.Collections;
using System.Collections.Generic;
using Bullet;
using Enemy;
using Extension;
using main.ui;
using UnityEditor;
using UnityEngine;
using Weapon;

namespace Player
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private AimIndicator _aimIndicator;

        [SerializeField] private float _maxAngle;

        [SerializeField] private float _currentAngle;

        [SerializeField] private Laser _laser;
        [SerializeField] private AimRocket _aimRocket;

        private Recoil _recoil;

        private Vector2 _startPos;
        private Vector2 _direction;
        private float _minDistanceTouch = 3f;
        private float _deceleration = 350f;

        private Vector2 _clampedDirection;
        private Vector2 _directionLeft = new Vector2(-0.5f, 0);
        private Vector2 _directionRight = new Vector2(0.5f, 0);
        
        private Shooting _shooting;
        private Zoom _zoom;
        private Recharge _recharge;
        private CompletionAim _completionAim;

        private float _partScreen;

        private bool _isAiming = false;

        private TypeWeapon _currentTypeWeapon;

        private BulletMover _bulletMover;

        private bool _isCheckFirstTouch;
        
        private void Awake()
        {
            _shooting = GetComponent<Shooting>();
            _zoom = Camera.main.gameObject.GetComponent<Zoom>();
            _recharge = GetComponent<Recharge>();

            _partScreen = Screen.width / 4f;

            _recoil = GetComponent<Recoil>();

            _completionAim = GetComponent<CompletionAim>();

            _currentAngle = 0;
        }

        private void Update()
        {
#if UNITY_EDITOR
            //PcInput();
#endif

#if UNITY_ANDROID
            MobileInput();
#endif
        }

        private void PcInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_recharge.IsRecharge())
                return;

                _zoom.SetZoom(false, true);
                        
                _startPos = Input.mousePosition;

                _shooting.TurnWeapon(false);
            }

            if (Input.GetMouseButton(0))
            {
                if (MainCanvasManager.Instance.IsShowedAimPanel())
                {
                    Check(Input.mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Shot();
            }
        }
        
        private void MobileInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                bool isRecharge = _recharge.IsRecharge();

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if(isRecharge)
                            return;

                        _isAiming = true;

                        _zoom.SetZoom(false, true);
                        
                        _startPos = touch.position;

                        _shooting.TurnWeapon(false);

                        if (_currentTypeWeapon == TypeWeapon.Laser)
                        {
                            _laser.StartLaser();
                        }

                        _isCheckFirstTouch = true;

                        break;

                    case TouchPhase.Moved:
                        if (MainCanvasManager.Instance.IsShowedAimPanel())
                        {
                            _direction = (touch.position - _startPos) / _deceleration;
                            
                            Check(touch.position);
                        }

                        if (_currentTypeWeapon == TypeWeapon.Minigun)
                        {
                            if (isRecharge == false)
                            {
                                CheckAimMinigun();
                            }
                        }

                        if (_currentTypeWeapon == TypeWeapon.Rocket)
                        {
                            CheckAimRocket();
                        }

                        break;

                    case TouchPhase.Stationary:
                        if (MainCanvasManager.Instance.IsShowedAimPanel())
                        {
                            float distance = Vector2.Distance(_startPos, touch.position);

                            if (distance > _minDistanceTouch)
                            {
                                if (_completionAim.enabled == false)
                                {
                                    _completionAim.StartCompletion(_startPos, touch.position, _minDistanceTouch);
                                }
                            }

                            _startPos = touch.position;
                        }
                        
                        if (_currentTypeWeapon == TypeWeapon.Minigun)
                        {
                            if (isRecharge == false)
                            {
                                CheckAimMinigun();
                            }
                        }

                        if (_currentTypeWeapon == TypeWeapon.Rocket)
                        {
                            CheckAimRocket();
                        }
                        
                        break;

                    case TouchPhase.Ended:

                        if (_currentTypeWeapon == TypeWeapon.Mine
                            || _currentTypeWeapon == TypeWeapon.Bulgarian
                            || _currentTypeWeapon == TypeWeapon.Sniper
                            || _currentTypeWeapon == TypeWeapon.Crossbow)
                            
                        {
                            if (isRecharge == false)
                            {
                                Shot();
                            }
                        }

                        if (_currentTypeWeapon != TypeWeapon.Rocket)
                        {
                            if (isRecharge == false)
                            {
                                StopAiming();
                            }
                        }
                        else
                        {
                            if (isRecharge == false && _bulletMover == null)
                            {
                                // RaycastHit hit;
                                // if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
                                // {
                                //     ///////////////////_aimIndicator.TurnShow(true);
                                //     ///////////////////_aimIndicator.UpdatePosition(hit.point);
                                // }
                                //
                                // Shot();
                                
                                StopAiming();
                            }
                        }

                        break;
                }

                // if (_isAiming)
                // {
                    // if (touch.position.x < _partScreen)
                    // {
                    //     if (_model.localRotation.eulerAngles.x - _direction.y >= 300 ||
                    //         _model.localRotation.eulerAngles.x - _direction.y <= 80f)
                    //     {
                    //         _clampedDirection = _directionLeft;
                    //
                    //         float tmpCurrentAngle = _currentAngle;
                    //         tmpCurrentAngle += _clampedDirection.x;
                    //
                    //         if (_currentAngle <= -_maxAngle || _currentAngle >= _maxAngle)
                    //         {
                    //             return;
                    //         }
                    //         else
                    //         {
                    //             _currentAngle = tmpCurrentAngle;
                    //         }
                    //         
                    //         _model.localRotation = Quaternion.Euler(
                    //             _model.localRotation.eulerAngles.x - _clampedDirection.y,
                    //             _model.localRotation.eulerAngles.y + _clampedDirection.x,
                    //             _model.localRotation.eulerAngles.z);
                    //     }
                    // }
                    //
                    // if (touch.position.x > Screen.width - _partScreen)
                    // {
                    //     if (_model.localRotation.eulerAngles.x - _direction.y >= 300 ||
                    //         _model.localRotation.eulerAngles.x - _direction.y <= 80f)
                    //     {
                    //         _clampedDirection = _directionRight;
                    //         
                    //         float tmpCurrentAngle = _currentAngle;
                    //         tmpCurrentAngle += _clampedDirection.x;
                    //
                    //         if (_currentAngle <= -_maxAngle || _currentAngle >= _maxAngle)
                    //         {
                    //             return;
                    //         }
                    //         else
                    //         {
                    //             _currentAngle = tmpCurrentAngle;
                    //         }
                    //         
                    //         _model.localRotation = Quaternion.Euler(
                    //             _model.localRotation.eulerAngles.x - _clampedDirection.y,
                    //             _model.localRotation.eulerAngles.y + _clampedDirection.x,
                    //             _model.localRotation.eulerAngles.z);
                    //     }
                    // }
                //}
            }
        }

        public void StopAiming()
        {
            if (_isCheckFirstTouch == false)
            {
                return;
            }
            else
            {
                _isCheckFirstTouch = false;
            }
            
            MainCanvasManager.Instance.ContinueGame();
            _zoom.SetZoom(true, true);
                        
            _isAiming = false;
            
            _bulletMover = null;
            
            if (_currentTypeWeapon == TypeWeapon.Rocket)
            {
                _aimIndicator.TurnShow(false);
                _aimRocket.StopCapture();
            }

            _completionAim.enabled = false;
            
            if (_currentTypeWeapon == TypeWeapon.Laser)
            {
                _recharge.StartRecharge();
                _recharge.OnRecharge();
                _laser.StopLaser();
            }
        }
        
        private void Check(Vector2 direction)
        {
            float distance = Vector2.Distance(_startPos, direction);

            if (distance > _minDistanceTouch)
            {
                if (_completionAim.enabled)
                {
                    _completionAim.enabled = false;
                }   
                
                Vector2 finalDirection = (direction - _startPos) / _deceleration;

                float targetEulerX = _model.localRotation.eulerAngles.x - finalDirection.y;
                float targetEulerY = _model.localRotation.eulerAngles.y + finalDirection.x;

                if (IsOutOfZone(finalDirection.x))
                {
                    targetEulerY = _model.localRotation.eulerAngles.y;
                }
                
                if (targetEulerX >= 300 || targetEulerX <= 80f)
                {
                    _model.localRotation = Quaternion.Euler(targetEulerX,
                        targetEulerY,
                        _model.localRotation.eulerAngles.z);
                }
            }
        }

        private void CheckPositionTarget()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                bool isHitEnemy = hit.collider.gameObject.TryGetComponent(out PartBody partBody) ||
                                  hit.collider.gameObject.TryGetComponent(out EnemyAttack enemyAttack);

                // if (_currentTypeWeapon == TypeWeapon.Minigun)
                // {
                //     if (isHitEnemy)
                //     {
                //         _shooting.Shot(hit.point, isHitEnemy, _recharge);
                //         _recharge.StartRecharge();
                //         _recharge.OnRecharge();
                //
                //         _recoil.enabled = true;
                //         
                //         StopAiming();
                //     }
                // }
                // else
                // {
                    if (_currentTypeWeapon != TypeWeapon.Rocket)
                    {
                        _shooting.Shot(hit.point, isHitEnemy, _recharge);
                        _recharge.StartRecharge();
                        _recharge.OnRecharge();
                    }
                //}
            }
        }

        private void Shot()
        {
            if (MainCanvasManager.Instance.IsShowedAimPanel())
            {
                if (_currentTypeWeapon != TypeWeapon.Laser)
                {
                    CheckPositionTarget();
                }
            }
            
            _shooting.TurnWeapon(true);

            if (_currentTypeWeapon != TypeWeapon.Minigun && _currentTypeWeapon != TypeWeapon.Laser)
            {
                _recoil.enabled = true;
            }
        }

        public void ShotRocket(Transform target)
        {
            StopAiming();
            
            _shooting.ShotRocket(target, _recharge);
            _recharge.StartRecharge();
            _recharge.OnRecharge();
            
            _shooting.TurnWeapon(true);
            _recoil.enabled = true;
        }

        private void CheckAimRocket()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 
                Mathf.Infinity))
            {
                bool isHitEnemy = hit.collider.gameObject.TryGetComponent(out PartBody partBody);
                bool isHouse = hit.collider.gameObject.TryGetComponent(out House house);

                if (isHitEnemy)
                {
                    _aimRocket.CaptureAim(partBody);
                }
                else
                {
                    if (isHouse)
                    {
                        _aimRocket.CaptureAim(house);
                    }
                    else
                    {
                        _aimRocket.StopCapture();
                    }
                }
            }
        }

        private void CheckAimMinigun()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 
                Mathf.Infinity))
            {
                bool isHitEnemy = hit.collider.gameObject.TryGetComponent(out PartBody partBody);

                if (isHitEnemy)
                {
                    StopAiming();
            
                    _shooting.ShotRocket(partBody.transform, _recharge);
                    _recharge.StartRecharge();
                    _recharge.OnRecharge();
            
                    _shooting.TurnWeapon(true);
                    _recoil.enabled = true;
                }
            }
        }

        public void SetTypeWeapon(TypeWeapon target)
        {
            _currentTypeWeapon = target;

            if (target != TypeWeapon.Laser)
            {
                _laser.HideLaser();
            }

            if (target != TypeWeapon.Rocket)
            {
                _aimRocket.gameObject.SetActive(false);
                _aimIndicator.enabled = false;
            }
        }

        public void SetBulletMover(BulletMover bulletMover)
        {
            _bulletMover = bulletMover;
        }

        private bool IsOutOfZone(float targetAngle)
        {
            bool isOut = false;

            float tmpCurrentAngle = _currentAngle;
            tmpCurrentAngle += targetAngle;

            if (targetAngle < 0)
            {
                if (_currentAngle <= -_maxAngle)
                {
                    isOut = true;
                }
                else
                {
                    _currentAngle = tmpCurrentAngle;
                }
            }
            else
            {
                if (targetAngle > 0)
                {
                    if (_currentAngle >= _maxAngle)
                    {
                        isOut = true;
                    }
                    else
                    {
                        _currentAngle = tmpCurrentAngle;
                    }
                }
            }


            return isOut;
        }
    }
}