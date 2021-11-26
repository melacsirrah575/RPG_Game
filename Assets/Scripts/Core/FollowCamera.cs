using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] GameObject target;

        void LateUpdate()
        {
            transform.position = target.transform.position;
        }
    }

}