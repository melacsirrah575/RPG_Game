using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        //Enumerable lets you run a foreach loop over it
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}
