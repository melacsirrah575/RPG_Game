using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            //Priority level: Check if alive, the check if I clicked on something I need to attack, then I don't move and vice-versa 
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            //Instead of having an 'out' return, RaycastAll returns a list of things hit
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            //Looping through all objects Raycast hit to look for enemy
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                //Continue carrys on to next index in the loop but skips the rest of the body of current index
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }

                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                //return is happening outside of if statement for future implmentation of Cursor changing
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            //Inlined variable
            RaycastHit hit;
            //out hit stores Raycast hit position in hit
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                //return is happening outside of if statement for future implmentation of Cursor changing
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //Takes position from where player clicked on MainCamera's near clipping plane and sets as variable
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
