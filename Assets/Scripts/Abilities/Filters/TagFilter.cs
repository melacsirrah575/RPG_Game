using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    [CreateAssetMenu(menuName = "Abilities/Filtering/Tag")]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = "";
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var gameObject in objectsToFilter) 
            {
                if (gameObject.CompareTag(tagToFilter))
                {
                    yield return gameObject;
                }
            }
        }
    }
}
