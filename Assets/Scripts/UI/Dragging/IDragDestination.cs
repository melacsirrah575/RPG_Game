using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Dragging
{
    public interface IDragDestination<T> where T : class
    {
        //How many of the given item can be accepted
        //If there is no limit Int.MaxValue should be returned
        int MaxAcceptable(T item);

        //Update the UI and any data relect adding the item to this destination
        void AddItems(T item, int number);
    }
}
