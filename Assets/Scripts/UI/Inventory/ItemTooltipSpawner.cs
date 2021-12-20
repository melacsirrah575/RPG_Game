using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.UI.Tooltips;

namespace RPG.UI.Inventories 
{
    [RequireComponent(typeof(IItemHolder))]
    public class ItemTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            var item = GetComponent<IItemHolder>().GetItem();
            if (!item) return false;

            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var itemToolTip = tooltip.GetComponent<ItemTooltip>();
            if (!itemToolTip) return;

            var item = GetComponent<IItemHolder>().GetItem();

            itemToolTip.Setup(item);
        }
    }

}
