using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion Item", menuName = "Inventory/Items/New Potion Item")]
public class Potion : ItemSObject
{
   
    private void Start()
    {
        itemType = ItemType.Potion;
    }
}
