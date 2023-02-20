using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// IPointerDownHandler - ������ �� ��������� ����� �� ������� �� ������� ����� ���� ������
/// IPointerUpHandler - ������ �� ����������� ����� �� ������� �� ������� ����� ���� ������
/// IDragHandler - ������ �� ��� �� ����� �� �� ������� ����� �� �������
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;

    private void Start()
    {
        //��������� ��� "PLAYER" �� ������� ���������!
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // ������� ������ InventorySlot � ����� � ��������
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ���� ���� ������, �� �� �� ��������� �� ��� ���� return;
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        //������ �������� ����������
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // ������ ��� ����� ������� ������ �� ������������ ��� ��������
        GetComponentInChildren<Image>().raycastTarget = false;
        // ������ ��� DraggableObject �������� InventoryPanel ����� DraggableObject ��� ��� ������� ������� ���������
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        // ������ �������� ����� �� ����������
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // � ����� ����� ����� ����� �� ������
        GetComponentInChildren<Image>().raycastTarget = true;

        //��������� DraggableObject ������� � ���� ������ ����
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //���� ����� �������� ��� �������� �� ����� UIPanel, ��...
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // ������ �������� �� ��������� - ������� ������ ������ ����� ����������
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
            // ������������� ���������� �������� ����� ����� ���� � �����
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            NullifySlotData();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            //���������� ������ �� ������ ����� � ������
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }

    }
    void NullifySlotData()
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmountText.text = "";
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemSObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        Text itemAmountText = newSlot.itemAmountText;

        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmountText.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmountText.text = "";
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
            oldSlot.itemAmountText.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmountText.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}