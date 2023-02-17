using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private StatsPlayer _strength;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPose;
    public float attackRange;
    public LayerMask whatIsEnemy;

    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                Collider[] enemisToDamage = Physics.OverlapSphere(attackPose.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemisToDamage.Length; i++)
                {
                    enemisToDamage[i].GetComponent<EnemyStats>().TakeDamage(_strength.Strength);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
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
