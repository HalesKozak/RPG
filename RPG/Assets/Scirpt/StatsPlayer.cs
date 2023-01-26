using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public int HP;
    public int MP;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bonus>(out var bonus) == true)
        {

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
