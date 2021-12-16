using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Dragging
{
    //Acts as both a source and destination for dragging.
    //If we are dragging between two containers it is possible to swap items
    public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {
    }
}
