using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public StatsPlayer _statsPlayer;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPose;
    public float attackRange;
    public LayerMask whatIsEnemy;

    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timeBtwAttack -= Time.deltaTime;
    }

    public void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            Collider[] enemisToDamage = Physics.OverlapSphere(attackPose.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemisToDamage.Length; i++)
            {
                enemisToDamage[i].GetComponent<EnemyStats>().TakeDamage(_statsPlayer.strength);
            }
        }
        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
