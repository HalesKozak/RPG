using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public GameObject particleDamage;
    public float HP;
    public float MP;
    public int Strength;

    public void TakeDamage(int damage)
    {
        StartCoroutine(ParticleDamagePlayer());
        HP -= damage;
    }

    IEnumerator ParticleDamagePlayer()
    {
        particleDamage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        particleDamage.SetActive(false);
    }
}
