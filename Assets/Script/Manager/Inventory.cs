using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Key")]

public class Inventory : ScriptableObject
{
    public string item_name;
    public Sprite item_image;
    public bool has_key;
}
