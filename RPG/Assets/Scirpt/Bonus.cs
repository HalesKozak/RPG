using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private GameObject image;
    public MeshRenderer render;
    public Collider trigercollider;
    public GameObject particle;

    public BonusType Type;

    public void PickUp()
    {
        StartCoroutine(Particle());
    }
    IEnumerator Particle()
    {
        image.SetActive(true);
        render.enabled=false;
        trigercollider.enabled = false;
        particle.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        image.SetActive(false);
        Destroy(gameObject);
    }
}
public enum BonusType { Slow, Speed, JumpBaff, JumpDebaff, HealthKit, ManaKit }