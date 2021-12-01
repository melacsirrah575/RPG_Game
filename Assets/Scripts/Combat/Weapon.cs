using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon: ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        public float WeaponDamage { get { return weaponDamage; } }
        [SerializeField] float weaponRange = 2f;
        public float WeaponRange { get { return weaponRange; } }

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;

            }
        }

    }
}
