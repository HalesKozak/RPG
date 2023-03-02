using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Potion, Weapon, Shield }
public class ItemSObject : ScriptableObject
{
    public ItemType itemType;
  
    public GameObject itemPrefab;
    public Sprite icon;

    public string itemName;
    public string itemDescription;
    public string inHandName;

    public bool isConsumeable;

    public int maximumAmount;

    [Header("Characteristics")]
    public float speedCount;
    public float jumpCount;
    public float healthCount;
    public float manaCount;
    public float damageCount;
}
