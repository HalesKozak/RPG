using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public MeshRenderer render;
    public Collider trigercollider;
    public GameObject particle;
    public GameObject image;

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
public enum BonusType { Slow, Speed, JumpBaff, JumpDebaff, HealthKit }