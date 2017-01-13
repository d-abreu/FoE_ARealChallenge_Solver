using System.Collections.Generic;

namespace ForgeOfEmpiresARealChallengeSolver
{
    class Buildings : List<Building>
    {
        public void Add(string name, int populationProvided)
        {
            Add(new Building() {Name = name, PopulationProvided = populationProvided});
        }

    }
}