using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int strength;

    public Image healthbar;
    private float fill;

    void Update()
    {
        fill = health/100f;
        healthbar.fillAmount = fill;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
