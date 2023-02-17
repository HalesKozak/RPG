using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string equipmentType;
    public int equipmentIndex;
    [Space]
    public Sprite itemSprite;
    [Space]
    public int amountInStack = 1;
    public int maxStackSize = 64;
    [Space]
    public int itemID;
}
