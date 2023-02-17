using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
        public Items slotsItems;

        public Sprite defaultSprite;
        public Text amountText;

        public void Start()
        {
            defaultSprite = GetComponent<Image>().sprite;
            amountText = transform.GetChild(0).GetComponent<Text>();
            amountText.text = "";
        }

        public void DropItem()
        {
            if (slotsItems)
            {
                slotsItems.transform.parent = null;
                slotsItems.gameObject.SetActive(true);
                slotsItems.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            }
        }

        public void CheckForItem()
        {
            if (transform.childCount > 1)
            {
                slotsItems = transform.GetChild(1).GetComponent<Items>();
                GetComponent<Image>().sprite = slotsItems.itemSprite;
                if (slotsItems.amountInStack > 1)
                    amountText.text = slotsItems.amountInStack.ToString();
            }
            else
            {
                slotsItems = null;
                GetComponent<Image>().sprite = defaultSprite;
                amountText.text = "";
            }
        }
}
