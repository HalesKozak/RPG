using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlots : MonoBehaviour
{

    public GameObject[] slot;
    public GameObject[] unstaticItems;
    public static GameObject[] items;

    public GameObject panel;

    public int number;
   
    void Start()
    {
        if (unstaticItems.Length > 0)
            items = unstaticItems;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            number = 0;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            number = 1;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            number = 2;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            number = 3;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            number = 4;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            number = 5;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            number = 6;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            number = 7;
            Equip();
        }
    }

    private void Equip()
    {
        for (int y = 0; y < slot.Length; y++)
        {
            if (y == number)
            {
                panel.transform.position = slot[y].transform.position;
                if (slot[y].transform.childCount > 1)
                {
                    Items item = slot[y].transform.GetChild(1).GetComponent<Items>();

                    if (item.equipmentType == "Axe")
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (i == item.equipmentIndex)
                            {
                                items[i].SetActive(!items[i].activeInHierarchy);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        {
                            items[i].SetActive(false);
                        }
                    }
                }
                break;
            }
        }
    }
}
