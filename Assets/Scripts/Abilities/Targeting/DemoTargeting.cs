using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.targeting
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Demo")]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action finished)
        {
            Debug.Log("Demo Targeting Started");
            finished();
        }
    }
}
