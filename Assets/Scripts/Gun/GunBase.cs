using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class GunBase : MonoBehaviour
{
    public ProjectilleBase projectilleBase;
    public Transform shootPosition;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;

    private Coroutine _currentCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(StartShoot());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
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
        var projectille = Instantiate(projectilleBase);
        projectille.transform.position = shootPosition.position;
        projectille.side = playerSideReference.transform.localScale.x;
    }
}
