using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            //Takes position from where player clicked on MainCamera's near clipping plane and sets as variable
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //out hit stores Raycast hit position in hit
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
