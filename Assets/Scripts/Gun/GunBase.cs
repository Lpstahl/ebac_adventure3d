using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;

    private Coroutine _currentCoroutine;
    //public Vector3 direction;
    //public float side = 1;

    public KeyCode keyCode = KeyCode.Z;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyCode))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        var projectille = Instantiate(prefabProjectile);
        projectille.transform.position = positionToShoot.position;
        projectille.transform.rotation = positionToShoot.rotation;
    }
}
