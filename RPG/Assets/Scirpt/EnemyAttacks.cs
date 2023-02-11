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

    public void Attack()
    {
        if (timeBtwAttack <= 0)
        {
                Collider[] playerToDamage = Physics.OverlapSphere(attackPose.position, attackRange, whatIsPlayer);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    playerToDamage[i].GetComponent<StatsPlayer>().TakeDamage(_strength.strength);
                }
                timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
