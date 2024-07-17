using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool isFlashlight;
    public bool isNote;
    public bool isEnDrink;
    public bool isFuse;
    public bool isRemote;
    public string paragraph;

    public string Paragraph { get => paragraph; set => paragraph = value; }
}
