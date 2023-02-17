using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour
{
    public Inventory inv;
    public Image followMouseImage;

    private GameObject curSlot;
    private Items curSlotsItem;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject obj = GetObjectUnderMouse();
            if (obj)
                obj.GetComponent<Slots>().DropItem();
        }
        if (Input.GetMouseButtonDown(0))
        {
            curSlot = GetObjectUnderMouse();
        }
        else if (Input.GetMouseButton(0))
        {
            followMouseImage.transform.position = Input.mousePosition;
            if (curSlot && curSlot.GetComponent<Slots>().slotsItems)
            {
                followMouseImage.color = new Color(255, 255, 255, 255);
                followMouseImage.sprite = curSlot.GetComponent<Image>().sprite;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (curSlot && curSlot.GetComponent<Slots>().slotsItems)
            {
                curSlotsItem = curSlot.GetComponent<Slots>().slotsItems;
                GameObject newObj = GetObjectUnderMouse();
                if (newObj && newObj != curSlot)
                {
                    if (newObj.GetComponent<EquipmentSlot>() && newObj.GetComponent<EquipmentSlot>().equipmentType != curSlotsItem.equipmentType)
                        return;

                    if (newObj.GetComponent<Slots>().slotsItems)
                    {
                        Items objectsItem = newObj.GetComponent<Slots>().slotsItems;
                        if (objectsItem.itemID == curSlotsItem.itemID && objectsItem.amountInStack != objectsItem.maxStackSize && !newObj.GetComponent<EquipmentSlot>())
                        {
                            curSlotsItem.transform.parent = null;
                            inv.AddItem(curSlotsItem, objectsItem);
                        }
                        else
                        {
                            objectsItem.transform.parent = curSlot.transform;
                            curSlotsItem.transform.parent = newObj.transform;
                        }
                    }
                    else
                    {
                        curSlotsItem.transform.parent = newObj.transform;
                    }
                }
            }
            foreach (Slots i in inv.equipSlots)
            {
                i.GetComponent<EquipmentSlot>().EquipSlot();
            }
        }
        else
        {
            followMouseImage.sprite = null;
            followMouseImage.color = new Color(0, 0, 0, 0);
        }
    }

    GameObject GetObjectUnderMouse()
    {
        GraphicRaycaster rayCaster = GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult i in results)
        {
            if (i.gameObject.GetComponent<Slots>())
                return i.gameObject;
        }
        return null;
    }
}
