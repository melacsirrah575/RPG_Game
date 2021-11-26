using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Interfaces act like contracts
    //Anything that implements this interface, needs to have these methods
    public interface IAction
    {
        void Cancel();
    }
}
