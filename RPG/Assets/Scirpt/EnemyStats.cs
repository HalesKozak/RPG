using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private GameObject prefabDead;
    public GameObject particleDamage;

    public int health;
    public int strength;

    public Image healthbar;
    private float fill;

    private void Update()
    {
        fill = health/100f;
        healthbar.fillAmount = fill;
        if (health <= 0)
        {
            StartCoroutine(ParticleDead());
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(ParticleDamage());
        health -= damage;
    }
    IEnumerator ParticleDamage()
    {
        particleDamage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        particleDamage.SetActive(false);
    }
    IEnumerator ParticleDead()
    {
        GameObject exposionRef = Instantiate(prefabDead);
        exposionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(2f);
        Destroy(exposionRef);
    }
}
