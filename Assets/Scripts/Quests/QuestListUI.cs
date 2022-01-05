using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] Quest[] tempQuests;
        [SerializeField] QuestItemUI questPrefab;
        void Start()
        {
            transform.DetachChildren();
            foreach (Quest quest in tempQuests)
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(quest);
            }
        }
    }
}
