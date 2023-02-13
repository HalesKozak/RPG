using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private EnemyStats _strength;

    public Transform attackPose;
    public float attackRange;
    public LayerMask whatIsPlayer;

    public void Attack()
    {
        Collider[] playerToDamage = Physics.OverlapSphere(attackPose.position, attackRange, whatIsPlayer);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].GetComponent<StatsPlayer>().TakeDamage(_strength.strength);
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
    }
}
