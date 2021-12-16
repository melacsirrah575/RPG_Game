using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//To be put on the icon representing an inventory item
//Allows the slot to update the icon and number
[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    public void SetItem(Sprite item)
    {
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            iconImage.enabled = false;
        } else
        {
            iconImage.enabled = true;
            iconImage.sprite = item;
        }
    }

    public Sprite GetItem()
    {
        var iconImage = GetComponent<Image>();
        if (!iconImage.enabled)
        {
            return null;
        }
        return iconImage.sprite;
    }
}
