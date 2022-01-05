using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Tooltips
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            
        }
    }
}
