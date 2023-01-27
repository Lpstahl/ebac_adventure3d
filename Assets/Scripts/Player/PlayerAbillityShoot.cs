using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbillityShoot : PlayerAbillityBase
{
    public GunBase gunbase;

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void StartShoot()
    {
        gunbase.StartShoot();
        Debug.Log("Shoot");
    }

    private void CancelShoot()
    {
        gunbase.StopShoot();
        Debug.Log("CancelShoot");
    }
}
