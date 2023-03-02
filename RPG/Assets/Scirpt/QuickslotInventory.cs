using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
    public StatsPlayer _statsPlayer;
    public InventorySlot activeSlot = null;

    public Transform quickslotParent;
    public Transform allWeapons;

    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    public int currentQuickslotID = 0;
  
    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        // ���������� �������� �����
        if (mw > 0.1)
        {
            // ����� ���������� ���� � ������ ��� �������� �� �������
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // ���� ������ ��������� ����� ������ � ���� ����� currentQuickslotID ����� ���������� �����, �� �������� ��� ������ ���� (������ ���� ��������� �������)
            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                currentQuickslotID++;
            }
            // ����� ���������� ���� � ������ ��� �������� �� "���������"
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();
            // ��� �� ������ � ���������:

        }
        else if (mw < -0.1)
        {
            // ����� ���������� ���� � ������ ��� �������� �� �������
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // ���� ������ ��������� ����� ����� � ���� ����� currentQuickslotID ����� 0, �� �������� ��� ��������� ����
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                currentQuickslotID--;
            }
            // ����� ���������� ���� � ������ ��� �������� �� "���������"
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();
            // ��� �� ������ � ���������:

        }
        // ���������� �����
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                var InvSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                var Img = quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>();
                if (currentQuickslotID == i)
                {
                    if (Img.sprite == notSelectedSprite)
                    {
                        Img.sprite = selectedSprite;
                        activeSlot = InvSlot;
                        ShowItemInHand();
                    }
                    else
                    {
                        Img.sprite = notSelectedSprite;
                        activeSlot = null;
                        HideItemInHand();
                    }
                }
                else
                {
                    Img.sprite = notSelectedSprite;
                    currentQuickslotID = i;

                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                    ShowItemInHand();
                }
            }
        }
        //���������� ������� �� ������� �� ����� ������ ����
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var InvSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            if (InvSlot.item != null)
            {
                if (InvSlot.item.isConsumeable && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    // ��������� ��������� � �������� (������� � ������ � �����) 
                    ChangeCharacteristics();

                    if (InvSlot.amount <= 1)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        InvSlot.amount--;
                        InvSlot.itemAmountText.text = InvSlot.amount.ToString();
                    }
                }
            }
        }
    }

    public void CheckItemInHand()
    {
        if (activeSlot != null)
        {
            ShowItemInHand();
        }
        else HideItemInHand();
    }

    private void ChangeCharacteristics()
    {
        var healthCount = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.healthCount;
        if (_statsPlayer.HP + healthCount <= 100)
        {
            _statsPlayer.HP += healthCount;
        }
        else
        {
            _statsPlayer.HP = 100;
        }
    }

    private void ShowItemInHand()
    {
        HideItemInHand();
        if(activeSlot.item == null)
        {
            return;
        }
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            if(activeSlot.item.inHandName == allWeapons.GetChild(i).name)
            {
                allWeapons.GetChild(i).gameObject.SetActive(true); 
            }
        }
    }

    private void HideItemInHand()
    {
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            allWeapons.GetChild(i).gameObject.SetActive(false);
        }
    }
}
