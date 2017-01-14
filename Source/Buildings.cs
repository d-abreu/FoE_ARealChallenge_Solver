using System.Collections.Generic;

namespace ForgeOfEmpiresARealChallengeSolver
{
    class Buildings : List<Building>
    {
        public void Add(string name, int populationProvided, bool isPremium = false)
        {
            Add(new Building()
            {
                Name = name,
                PopulationProvided = populationProvided,
                IsPremium = isPremium
            });
        }

    }
}