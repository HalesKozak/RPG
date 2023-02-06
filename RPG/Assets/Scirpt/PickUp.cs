using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public DataBase data;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Weapon>(out var weapon) == true)
        {
            if (weapon.Type == WeaponType.Axe)
            {
                TryGetComponent<Inventory>(out var item);
                item.AddItem(3, data.items[3],2);
            }
        }
    }
}
