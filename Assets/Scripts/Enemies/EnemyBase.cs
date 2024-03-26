using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; // Biblioteca que pode ser removida se não estiver sendo usada
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Referência para o componente Animator do inimigo
    public Animator animator;
    // Nome do gatilho de animação de ataque no Animator
    public string triggerToAttack = "Attack";
    public string triggerToDeath = "Death";

    // Quantidade de dano causado pelo inimigo ao atacar
    public float timeToDeath= 1f;
    public int damage = 10;
    public HealthBase healthBase;

    void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        PlayerDeathAnimation();
        Destroy(gameObject, timeToDeath);
    }
    // Método chamado quando ocorre uma colisão com outro objeto 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Obtém o componente HealthBase do objeto colidido (se existir)
        var health = collision.gameObject.GetComponent<HealthBase>();

        // Verifica se o componente HealthBase foi encontrado
        if (health != null)
        {
            // Causa dano ao objeto colidido chamando o método Damage do componente HealthBase
            health.Damage(damage);
            // Chama a animação de ataque do inimigo
            PlayerAttackAnimation();
        }

    }

    // Método para iniciar a animação de ataque do inimigo
    private void PlayerAttackAnimation()
    {
        // Define o gatilho de ataque no Animator para iniciar a animação correspondente
        animator.SetTrigger(triggerToAttack);
    }
    private void PlayerDeathAnimation()
    {
        // Define o gatilho de ataque no Animator para iniciar a animação correspondente
        animator.SetTrigger(triggerToDeath);
    }


    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
