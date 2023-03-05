using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private float lifeTime;
    public Transform spawnPoint;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Instantiate(prefabEnemy, spawnPoint.position, Quaternion.identity);
            Debug.Log("SpawnEnemy");
            Debug.Log(prefabEnemy.transform.position);
            Destroy(gameObject);
        }
    }
}
