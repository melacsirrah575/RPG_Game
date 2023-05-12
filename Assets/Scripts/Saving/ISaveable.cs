
//The Interface that any object wanting to be saved must inherit from

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}
