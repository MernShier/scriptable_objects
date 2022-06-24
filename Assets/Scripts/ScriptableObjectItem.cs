using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyItem", menuName = "ScriptableObjects/ScriptableObjectItem", order = 100)]
public class ScriptableObjectItem : ScriptableObject
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