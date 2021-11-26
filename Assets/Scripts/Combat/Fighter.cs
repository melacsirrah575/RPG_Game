using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        Mover mover;

        float timeSinceLastAttack;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if(target.HasDied()) { return; }

                if (target != null && !GetIsInRange())
                {
                    mover.MoveTo(target.transform.position);
                }
                else
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
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
            target.TakeDamage(weaponDamage);
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
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
