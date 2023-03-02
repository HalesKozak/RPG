using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject UIBG;
    public Transform inventoryPanel;
    public Transform quickSlotPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private Camera mainCamera;

    public bool isOpened;
    public float reachDistance = 3f;

    private void Awake()
    {
        UIBG.SetActive(true);
    }
    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            var invSlotScript = inventoryPanel.GetChild(i).GetComponent<InventorySlot>();
            if (invSlotScript != null)
            {
                slots.Add(invSlotScript);
            }
        }
        for (int i = 0; i < quickSlotPanel.childCount; i++)
        {
            var invSlotScript = quickSlotPanel.GetChild(i).GetComponent<InventorySlot>();
            if (invSlotScript != null)
            {
                slots.Add(invSlotScript);
            }
        }
        UIBG.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIBG.SetActive(true);
                inventoryPanel.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {
                UIBG.SetActive(false);
                inventoryPanel.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                var itemScript = hit.collider.gameObject.GetComponent<Item>();
                if (itemScript != null)
                {
                    AddItem(itemScript.item, itemScript.amount);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    private void AddItem(ItemSObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }
                break;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                if (slot.item.maximumAmount != 1)
                {
                    slot.itemAmountText.text = _amount.ToString();
                }
                break;
            }
        }
    }
}
