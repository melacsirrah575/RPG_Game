using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Dragging
{
    // <typeparam name="T">The type that represents the item being dragged.</typeparam>

    public interface IDragSource<T> where T : class
    {
        T GetItem();

        int GetNumber();

        void RemoveItems(int number);
    }
}
