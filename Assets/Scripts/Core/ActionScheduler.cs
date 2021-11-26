using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;
        //Using Substitution principle (Subbing a Derrived Type instead of the parent type) so we can cancel the action at the right time
        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                print("Cancelling" + currentAction);
            }
            currentAction = action;
        }
    }
}
