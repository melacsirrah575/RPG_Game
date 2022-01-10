using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Demo")]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting()
        {
            Debug.Log("Demo Targeting Started");
        }
    }
}
