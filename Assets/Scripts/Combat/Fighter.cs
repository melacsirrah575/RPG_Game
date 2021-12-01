using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;


        Health target;
        Mover mover;

        float timeSinceLastAttack;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Start()
        {
            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if(target.HasDied()) { return; }

                if (target != null && !GetIsInRange())
                {
                    mover.MoveTo(target.transform.position, 1f);
                }
                else
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
        }

        private void SpawnWeapon()
        {
            if(weapon == null) return;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.WeaponRange;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggerAttack();
                //Allows player to attack immediately when first reaching enemy
                timeSinceLastAttack = Mathf.Infinity;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Hit is an Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weapon.WeaponDamage);
        }

        //Determines if we can attack the current object in the list of objects hit by Raycast in PlayerController
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.HasDied();
        }

        //Player Controller sets target in Attack and calls function
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            mover.Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
