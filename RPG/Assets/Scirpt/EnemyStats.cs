using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private GameObject prefabDead;
    [SerializeField] private GameObject prefabSpawnerEnemy;
    [SerializeField] private AudioSource takeDamageClipEnemy;
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
            GameObject exposionRef = Instantiate(prefabDead);
            exposionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(prefabSpawnerEnemy);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamageClipEnemy.Play();
        StartCoroutine(ParticleDamage());
    }
    IEnumerator ParticleDamage()
    {
        particleDamage.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        particleDamage.SetActive(false);
    }
}
