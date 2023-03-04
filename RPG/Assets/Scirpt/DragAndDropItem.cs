using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public InventorySlot oldSlot;
    public Transform player;
    private QuickslotInventory _quickslotInventory;

    private void Start()
    {
        _quickslotInventory = FindObjectOfType<QuickslotInventory>();
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        transform.SetParent(transform.parent.parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        transform.position = Input.mousePosition; 
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIBG")
        {
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + player.forward, Quaternion.identity);
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            NullifySlotData();
            _quickslotInventory.CheckItemInHand();
        }
        else ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        _quickslotInventory.CheckItemInHand();
    }

    public void NullifySlotData()
    {
        var OldIconGOImg = oldSlot.iconGO.GetComponent<Image>();
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        OldIconGOImg.color = new Color(1, 1, 1,0);
        OldIconGOImg.sprite = null;
        oldSlot.itemAmountText.text = "";
    }
    private void ExchangeSlotData(InventorySlot newSlot)
    {
        var OldIconGOImg = oldSlot.iconGO.GetComponent<Image>();
        var NewIconGOImg = newSlot.iconGO.GetComponent<Image>();
        ItemSObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        Text itemAmountText = newSlot.itemAmountText;

        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(OldIconGOImg.sprite);
            if (oldSlot.item.maximumAmount !=1)
            {
                newSlot.itemAmountText.text = oldSlot.amount.ToString();
            }
            else newSlot.itemAmountText.text = "";

        }
        else
        {
            NewIconGOImg.color = new Color(1, 1, 1, 0);
            NewIconGOImg.sprite = null;
            newSlot.itemAmountText.text = "";
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(item.icon);
            if (oldSlot.item.maximumAmount != 1)
            {
                newSlot.itemAmountText.text = oldSlot.amount.ToString();
            }
            else newSlot.itemAmountText.text = "";
        }
        else
        {
            OldIconGOImg.color = new Color(1, 1, 1, 0);
            OldIconGOImg.sprite = null;
            oldSlot.itemAmountText.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}
