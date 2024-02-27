using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Weapon,
    Potion,
    Armor
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class ItemObject : ScriptableObject
{
    public int id;
    public string name;
    public Sprite sprite;
    public ItemType type;
    public bool stackable;
}
