using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogues
{
    [System.Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
    }
}
