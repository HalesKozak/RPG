using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Inventory : MonoBehaviour
{
        public GameObject inventoryObject;
        public float distance = 2f;

        public Slots[] slots;

        public Slots[] equipSlots;

        void Start()
        {
            inventoryObject.SetActive(false);

            foreach (Slots i in slots)
            {
                i.Start();
            }
            foreach (Slots i in equipSlots)
                i.Start();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryObject.activeSelf == false)
                {
                    inventoryObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                   // gameObject.GetComponent<FirstPersonController>().enabled = false;
                }
                else
                {
                    inventoryObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    //gameObject.GetComponent<FirstPersonController>().enabled = true;
                }
            }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.gameObject.GetComponent<Items>())
                    AddItem(hit.collider.gameObject.GetComponent<Items>());
            }
        }

        foreach (Slots i in slots)
            {
                i.CheckForItem();
            }
            foreach (Slots i in equipSlots)
                i.CheckForItem();
        }

        public int GetItemAmount(int id)
        {
            int num = 0;
            foreach (Slots i in slots)
            {
                if (i.slotsItems)
                {
                    Items z = i.slotsItems;
                    if (z.itemID == id)
                        num += z.amountInStack;
                }
            }
            return num;
        }

        public void RemoveItemAmount(int id, int amountToRemove)
        {
            foreach (Slots i in slots)
            {
                if (amountToRemove <= 0)
                    return;

                if (i.slotsItems)
                {
                    Items z = i.slotsItems;
                    if (z.itemID == id)
                    {
                        int amountThatCanRemoved = z.amountInStack;
                        if (amountThatCanRemoved <= amountToRemove)
                        {
                            Destroy(z.gameObject);
                            amountToRemove -= amountThatCanRemoved;
                        }
                        else
                        {
                            z.amountInStack -= amountToRemove;
                            amountToRemove = 0;
                        }
                    }
                }
            }
        }

        public void AddItem(Items itemToBeAdded, Items startingItem = null)
        {
            int amountInStack = itemToBeAdded.amountInStack;
            List<Items> stackableItems = new List<Items>();
            List<Slots> emptySlots = new List<Slots>();

            if (startingItem && startingItem.itemID == itemToBeAdded.itemID && startingItem.amountInStack < startingItem.maxStackSize)
                stackableItems.Add(startingItem);

            foreach (Slots i in slots)
            {
                if (i.slotsItems)
                {
                    Items z = i.slotsItems;
                    if (z.itemID == itemToBeAdded.itemID && z.amountInStack < z.maxStackSize && z != startingItem)
                        stackableItems.Add(z);
                }
                else
                {
                    emptySlots.Add(i);
                }
            }

            foreach (Items i in stackableItems)
            {
                int amountThatCanbeAdded = i.maxStackSize - i.amountInStack;
                if (amountInStack <= amountThatCanbeAdded)
                {
                    i.amountInStack += amountInStack;
                    Destroy(itemToBeAdded.gameObject);
                    return;
                }
                else
                {
                    i.amountInStack = i.maxStackSize;
                    amountInStack -= amountThatCanbeAdded;
                }
            }

            itemToBeAdded.amountInStack = amountInStack;
            if (emptySlots.Count > 0)
            {
                itemToBeAdded.transform.parent = emptySlots[0].transform;
                itemToBeAdded.gameObject.SetActive(false);
            }
        }
    //    public DataBase data;

    //    public List<ItemInventory> items = new List<ItemInventory>();

    //    public GameObject gameObjShow;
    //    public GameObject InventoryMainObj;
    //    public GameObject background;
    //    public GameObject hotBar;

    //    public int maxCount;
    //    public int maxCountHotBar;

    //    public Camera cam;
    //    public EventSystem es;

    //    public int currentID;
    //    public ItemInventory currentItem;

    //    public RectTransform movingObject;
    //    public Vector3 offset;

    //    public void Start()
    //    {
    //        if (items.Count==0)
    //        {
    //            AddGraphics();
    //        }
    //        UpdateInventory();
    //    }

    //    public void Update()
    //    {
    //        if (currentID != -1)
    //        {
    //            MoveObject();
    //        }
    //        if (Input.GetKeyDown(KeyCode.I))
    //        {
    //            Cursor.lockState = CursorLockMode.Confined;
    //            background.SetActive(!background.activeSelf);
    //            hotBar.SetActive(!hotBar.activeSelf);
    //            if(background.activeSelf)
    //            {
    //                UpdateInventory();
    //            }
    //        }
    //        else if(!background.activeSelf)
    //        {
    //            Cursor.lockState = CursorLockMode.Locked;
    //            CopyHotBar();
    //        }
    //    }

    //    public void SearchForSameItem(Item item, int count)
    //    {
    //        for (int i = 0; i < maxCount; i++)
    //        {
    //            if(items[i].id == item.id)
    //            {
    //                if (items[i].count < 64)
    //                {
    //                    items[i].count += count;
    //                    if (items[i].count > 64)
    //                    {
    //                        count = items[i].count - 128;
    //                        items[i].count = 64;
    //                    }
    //                    else
    //                    {
    //                        count = 0;
    //                        i = maxCount;
    //                    }
    //                }
    //            }
    //        }
    //        if (count > 0)
    //        {
    //            for (int i = 0; i < maxCount; i++)
    //            {
    //                if (items[i].id == 0)
    //                {
    //                    AddItem(i, item, count);
    //                    i = maxCount;
    //                }
    //            }
    //        }
    //    }
    //    public void AddItem(int id, Item item, int count)
    //    {
    //        items[id].id = item.id;
    //        items[id].count = count;
    //        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[item.id].img;

    //        if (count>1 && item.id != 0)
    //        {
    //            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
    //        }
    //        else
    //        {
    //            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    //        }
    //    }

    //    public void AddInventoryItem(int id, ItemInventory invItem)
    //    {
    //        items[id].id = invItem.id;
    //        items[id].count = invItem.count;
    //        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

    //        if (invItem.count > 1 && invItem.id != 0)
    //        {
    //            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
    //        }
    //        else
    //        {
    //            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    //        }
    //    }

    //    public void AddGraphics()
    //    {
    //        for (int i = 0; i < maxCount; i++)
    //        {
    //            GameObject newItem = Instantiate(gameObjShow, InventoryMainObj.transform) as GameObject;

    //            newItem.name = i.ToString();

    //            ItemInventory ii = new ItemInventory();
    //            ii.itemGameObj = newItem;

    //            RectTransform rt = newItem.GetComponent<RectTransform>();
    //            rt.localPosition = new Vector3(0, 0, 0);
    //            rt.localScale = new Vector3(1, 1, 1);
    //            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

    //            Button tempButton = newItem.GetComponent<Button>();

    //            tempButton.onClick.AddListener(delegate { SelectObject(); });

    //            items.Add(ii);
    //        }
    //        for (int i = 0; i < maxCountHotBar; i++)
    //        {
    //            GameObject newItem = Instantiate(gameObjShow, hotBar.transform) as GameObject;

    //            newItem.name = i.ToString();

    //            ItemInventory ii = new ItemInventory();
    //            ii.itemGameObj = newItem;

    //            RectTransform rt = newItem.GetComponent<RectTransform>();
    //            rt.localPosition = new Vector3(0, 0, 0);
    //            rt.localScale = new Vector3(1, 1, 1);
    //            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

    //            Button tempButton = newItem.GetComponent<Button>();

    //            tempButton.onClick.AddListener(delegate { SelectObject(); });

    //            items.Add(ii);
    //        }
    //    }

    //    public void UpdateInventory()
    //    {
    //        for (int i = 0; i < maxCount; i++)
    //        {
    //            if(items[i].id !=0 && items[i].count > 1)
    //            {
    //                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
    //            }
    //            else
    //            {
    //                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
    //            }

    //            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
    //        }
    //    }
    //    public void SelectObject()
    //    {
    //        if(currentID == -1)
    //        {
    //            if (items[int.Parse(es.currentSelectedGameObject.name)].id == 0)
    //            {
    //                return;
    //            }
    //            currentID = int.Parse(es.currentSelectedGameObject.name);
    //            currentItem = CopyInventoryItem(items[currentID]);
    //            movingObject.gameObject.SetActive(true);
    //            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

    //            AddItem(currentID, data.items[0], 0);
    //        }
    //        else
    //        {
    //            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
    //            if (currentItem.id !=II.id)
    //            {
    //                AddInventoryItem(currentID, II);
    //                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
    //            }
    //            else
    //            {
    //                if (II.count + currentItem.count <= 64)
    //                {
    //                    II.count += currentItem.count;
    //                }
    //                else
    //                {
    //                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 64);
    //                    II.count = 64;
    //                }
    //                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
    //            }
    //            currentID = -1;

    //            movingObject.gameObject.SetActive(false);
    //        }
    //    }

    //    public void MoveObject()
    //    {
    //        Vector3 mousePosition = Input.mousePosition;
    //        mousePosition.z = 0.35f;
    //        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //        movingObject.position = worldPosition;
    //    }

    //    public void CopyHotBar()
    //    {

    //    }

    //    public ItemInventory CopyInventoryItem(ItemInventory old)
    //    {
    //        ItemInventory New = new ItemInventory();

    //        New.id = old.id;
    //        New.itemGameObj = old.itemGameObj;
    //        New.count = old.count;
    //        return New;
    //    }
    //}

    //public class ItemInventory
    //{
    //    public int id;
    //    public int count;

    //    public GameObject itemGameObj;
    //    public Sprite img;
}
