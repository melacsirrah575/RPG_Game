using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public interface IPredicateEvaluator
    {
        //Question mark after bool signifies that it can also answer IDK
        bool? Evaluate(string predicate, string[] parameters);
    }
}
