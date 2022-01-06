using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI questPrefab;
        void Start()
        {
            transform.DetachChildren();
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }
    }
}
