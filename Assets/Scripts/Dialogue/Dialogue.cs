using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogues
{
    [CreateAssetMenu(menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] DialogueNode[] nodes;
    }
}
