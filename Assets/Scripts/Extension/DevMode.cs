using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

public class DevMode : MonoBehaviour
{
    [SerializeField] private Button _rocket, _sniper, _minigun, _bulgarian, _laser, _offTimer;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rocket.onClick.AddListener(Rocket);
        _sniper.onClick.AddListener(Sniper);
        _minigun.onClick.AddListener(Minigun);
        _bulgarian.onClick.AddListener(Bulgarian);
        _laser.onClick.AddListener(Laser);
        _offTimer.onClick.AddListener(TurnTimer);
    }

    private void Rocket()
    {
        Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Rocket);
        Hide();
    }

    private void Sniper()
    {
        Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Sniper);
        Hide();
    }

    private void Minigun()
    {
        Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Minigun);
        Hide();
    }

    private void Bulgarian()
    {
        Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Bulgarian);
        Hide();
    }

    private void Laser()
    {
        Weapon.ChoicedWeapon.Instance.Init(TypeWeapon.Laser);
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void TurnTimer()
    {
        GameTimer.Instance.OffTimer();
    }
}
