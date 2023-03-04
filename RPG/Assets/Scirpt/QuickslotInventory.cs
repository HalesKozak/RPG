using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
    public StatsPlayer _statsPlayer;
    public PlayerMovement _playerMovement;
    public InventorySlot activeSlot = null;

    public Transform quickslotParent;
    public Transform allWeapons;

    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    public int currentQuickslotID = 0;


    private void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        // Используем колесико мышки
        if (_playerMovement.isAction == false)
        {
            if (mw > 0.1)
            {
                // Берем предыдущий слот и меняем его картинку на обычную
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                // Если крутим колесиком мышки вперед и наше число currentQuickslotID равно последнему слоту, то выбираем наш первый слот (первый слот считается нулевым)
                if (currentQuickslotID >= quickslotParent.childCount - 1)
                {
                    currentQuickslotID = 0;
                }
                else
                {
                    currentQuickslotID++;
                }
                // Берем предыдущий слот и меняем его картинку на "выбранную"
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;

                activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                ShowItemInHand();
                // Что то делаем с предметом:

            }
            else if (mw < -0.1)
            {
                // Берем предыдущий слот и меняем его картинку на обычную
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                // Если крутим колесиком мышки назад и наше число currentQuickslotID равно 0, то выбираем наш последний слот
                if (currentQuickslotID <= 0)
                {
                    currentQuickslotID = quickslotParent.childCount - 1;
                }
                else
                {
                    currentQuickslotID--;
                }
                // Берем предыдущий слот и меняем его картинку на "выбранную"
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                ShowItemInHand();

            }
            // Используем цифры
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
        }
    }
    public void CheckAmountItem()
    {
        var InvSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
        if (InvSlot.amount <= 1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
        }
        else
        {
            InvSlot.amount--;
            InvSlot.itemAmountText.text = InvSlot.amount.ToString();
        }
        CheckItemInHand();
    }
    public void CheckItemInHand()
    {
        if (activeSlot != null)
        {
            ShowItemInHand();
        }
        else HideItemInHand();
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
