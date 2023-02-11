using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private EnemyStats _strength;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPose;
    public float attackRange;
    public LayerMask whatIsPlayer;

    public Animator animator;

    public void Attack()
    {
        animator.SetBool("Attack", true);
        if (timeBtwAttack <= 0)
        {
            //animator.SetBool("Attack", true);
            Collider[] playerToDamage = Physics.OverlapSphere(attackPose.position, attackRange, whatIsPlayer);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].GetComponent<StatsPlayer>().TakeDamage(_strength.strength);
            }
            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            animator.SetBool("Attack", false);
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
