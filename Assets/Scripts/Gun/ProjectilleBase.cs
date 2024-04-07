using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilleBase : MonoBehaviour
{
    // Direção do projétil
    public Vector3 direction;
    public float side = 1;
    public AudioSource audioSource;
    [SerializeField] private int bulletDamage = 3;
    [SerializeField] private float timToDestroy = .7f;

    void Start()
    {
        Play();    
    }
    // Método chamado a cada quadro de atualização
    void Update()
    {
        // Move o projétil na direção especificada
        transform.Translate(direction * Time.deltaTime * side);
        timeToDestroy();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.transform.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            Debug.Log(bulletDamage);
            enemy.Damage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void timeToDestroy()
    {
        Destroy(gameObject, timToDestroy);
    }

    void Play()
    {
        audioSource.Play();
    }
}
