using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Potion, Weapon }
public class ItemSObject : ScriptableObject
{
    public string itemName;
    public int maximumAmount;
    public GameObject itemPrefab;
    public Sprite icon;
    public ItemType itemType;
    public string itemDescription;
    public bool isConsumeable;

    public float speedCount;
    public float jumpCount;
    public float healthCount;
    public float manaCount;
    public float damage;
}
