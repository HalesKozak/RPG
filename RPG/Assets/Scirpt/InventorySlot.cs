using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemSObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public Text itemAmountText;

    private void Awake()
    {
        iconGO = transform.GetChild(0).GetChild(0).gameObject;
        itemAmountText = transform.GetChild(0).GetChild(1).GetComponent<Text>();

    }
    public void SetIcon(Sprite icon)
    {
        var iconGOImg = iconGO.GetComponent<Image>();
        iconGOImg.color = new Color(1, 1, 1, 1);
        iconGOImg.sprite = icon;
    }
}
