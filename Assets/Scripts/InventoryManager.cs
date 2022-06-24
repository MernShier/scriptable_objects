using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = System.Random;

public class InventoryManager : MonoBehaviour
{
    public ScriptableObjectItem[] itemsScriptableParameters;
    public GameObject[] item_Slots;
    public Text[] texts;
    [SerializeField] private ItemData[] itemData;
    [SerializeField] private int i;
    Random rnd = new Random();
    private void Awake()
    {
        itemData = new ItemData[item_Slots.Length];
        if (PlayerPrefs.HasKey("Save_0"))
        {
            for (int ii = 0; ii < item_Slots.Length; ii++)
            {
                if (PlayerPrefs.HasKey($"Save_{ii}"))
                {
                    itemData[ii] = JsonUtility.FromJson<ItemData>(PlayerPrefs.GetString($"Save_{ii}"));
                    if (itemData[ii] != null)
                    {
                        item_Slots[ii].GetComponent<Image>().sprite = itemData[ii].Icon;
                    }
                    Console.WriteLine($"Save_{ii}");
                }
            }
        }
    }
    private void FixedUpdate()
    {
    }
    public void GenerateItem(int ChestType)
    {
        int currentRnd = rnd.Next(0, itemsScriptableParameters.Length);
        int voidRnd = rnd.Next(1, 21);
        int HungryChestFinder = 0;
        if (ChestType == 1)
        {
            int hungryChestRnd = rnd.Next(1, 3);
            if (hungryChestRnd == 1)
            {
                for (int u = item_Slots.Length - 1; u > rnd.Next(3, 7); u--)
                {
                    Debug.Log($"chest hungry, u = {u}");
                    HungryChestFinder = u;
                    while (HungryChestFinder >= 0)
                    {
                        if (itemData[HungryChestFinder] != null)
                        {
                            itemData[HungryChestFinder] = null;
                            item_Slots[HungryChestFinder].GetComponent<Image>().sprite = null;
                            break;
                        }
                        HungryChestFinder--;
                    }
                }
                i = 0;
            }
        }
        if (voidRnd == 1)
        {
            Debug.Log("Void");
            DestroyItems();
            return;
        }
        if (i < item_Slots.Length)
        {
            if (itemData[i] == null)
            {
                itemData[i] = new ItemData();
                itemData[i].Icon = itemsScriptableParameters[currentRnd].Icon;
                itemData[i].itemType = (ItemData.ItemTypes)itemsScriptableParameters[currentRnd].itemType;
                itemData[i].Name = itemsScriptableParameters[currentRnd].Name;
                itemData[i].Cost = itemsScriptableParameters[currentRnd].Cost;
                itemData[i].SpecialValue = itemsScriptableParameters[currentRnd].SpecialValue;
                item_Slots[i].GetComponent<Image>().sprite = itemData[i].Icon;
                i++;
            }
            else
            {
                while (itemData[i] != null && i < item_Slots.Length)
                {
                    i++;
                }
                GenerateItem(0);
            }
        }
        Save();
    }
    public void DestroyItems()
    {
        for (int ii = 0; ii <= item_Slots.Length - 1; ii++)
        {
            itemData[ii] = null;
            item_Slots[ii].GetComponent<Image>().sprite = null;
        }
        i = 0;
        Save();
    }
    public void Save()
    {
        int i = 0;
        foreach (ItemData item in itemData)
        {
            PlayerPrefs.SetString($"Save_{i}", JsonUtility.ToJson(item));
            i++;
        }
        i = 0;
    }
    public void MouseEnterText(int number)
    {
        if (itemData[number] != null)
        {
            texts[0].text = itemData[number].Name;
            texts[1].text = itemData[number].itemType.ToString();
            texts[2].text = $"Price: {itemData[number].Cost}";

            if (itemData[number].itemType == ItemData.ItemTypes.Armour)
            {
                texts[3].text = $"Defence: {itemData[number].SpecialValue}";
            }
            else if (itemData[number].itemType == ItemData.ItemTypes.Weapon)
            {
                texts[3].text = $"Damage: {itemData[number].SpecialValue}";
            }
            else if (itemData[number].itemType == ItemData.ItemTypes.Consumable)
            {
                texts[3].text = $"Healing: {itemData[number].SpecialValue}";
            }
            else if (itemData[number].itemType == ItemData.ItemTypes.Material)
            {
                texts[3].text = $"Durability: {itemData[number].SpecialValue}";
            }
        }
    }
    public void MouseExitText()
    {
        foreach (Text text in texts)
        {
            text.text = null;
        }
    }
    public void MouseDown(int number)
    {
        if (itemData[number] != null)
        {
            item_Slots[number].GetComponent<Image>().sprite = null;
            itemData[number] = null;
        }
        i = 0;
        Save();
    }
    public class ItemData
    {
        public Sprite Icon;
        public ItemTypes itemType;
        public string Name;
        public int Cost, SpecialValue;
        public enum ItemTypes
        {
            Armour = 1,
            Weapon,
            Consumable,
            Material
        }
    }
}
