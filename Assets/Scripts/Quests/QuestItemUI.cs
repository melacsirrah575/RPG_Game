using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;
        public void Setup(Quest quest)
        {
            title.text = quest.GetTitle();
            progress.text = "0/" + quest.GetObjectiveCount();
        }
    }
}
