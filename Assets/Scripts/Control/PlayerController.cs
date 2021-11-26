using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Combat;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            //Instead of having an 'out' return, RaycastAll returns a list of things hit
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            //Looping through all objects Raycast hit to look for enemy
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                //Continue carrys on in the loop but skips the body it was put it
                if (target == null) continue;
                //Don't want player to be able to hold down mouse to attack
                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            //Inlined variable
            RaycastHit hit;
            //out hit stores Raycast hit position in hit
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            //Takes position from where player clicked on MainCamera's near clipping plane and sets as variable
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
