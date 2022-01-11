using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;

namespace RPG.Abilities.targeting
{
    [CreateAssetMenu(menuName ="Abilities/Targeting/Directional")]
    public class DirectionalTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] float groundOffset = 1f;
        public override void StartTargeting(AbilityData data, Action finished)
        {
            RaycastHit raycastHit;
            Ray ray = PlayerController.GetMouseRay();
            if (Physics.Raycast(ray, out raycastHit, 1000, layerMask))
            {
                data.SetTargetedPoint(raycastHit.point + ray.direction * groundOffset / ray.direction.y);
            }
            finished();
        }
    }

}