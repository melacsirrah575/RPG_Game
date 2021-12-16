using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Dragging
{
    // <typeparam name="T">The type that represents the item being dragged.</typeparam>

    public interface IDragSource<T> where T : class
    {
        // What item type currently resides in this source?
        T GetItem();

        // What is the quantity of items in this source?
        int GetNumber();

        // Remove a given number of items from the source. Should never exceed the number returned by 'GetNumber'
        void RemoveItems(int number);
    }
}
