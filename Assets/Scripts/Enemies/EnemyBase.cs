using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Animator animator;
    public string triggerToAttack = "Attack";
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damage);
        }
        
        PlayerAttackAnimation();
    }

    private void PlayerAttackAnimation()
    {
        animator.SetTrigger(triggerToAttack);
    }
}
