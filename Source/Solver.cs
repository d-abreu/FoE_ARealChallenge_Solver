using System;
using System.Collections.Generic;
using System.Linq;

namespace ForgeOfEmpiresARealChallengeSolver
{
    internal class Solver
    {
        private readonly int _currentPopulation;
        private Buildings _buildings;
        private const int TargetPopulation = 1997;

        public Solver(int currentPopulation)
        {
            _currentPopulation = currentPopulation;
            _buildings = LoadBuildings();
        }

        private Buildings LoadBuildings()
        {
            var buildings = new Buildings();
            buildings.Add("Hut", 14);
            buildings.Add("Stilt House", 22);
            buildings.Add("Chalet", 32);
            buildings.Add("Thatched House", 27);
            buildings.Add("Villa", 87);
            buildings.Add("Roof Tile House", 44);
            buildings.Add("Cottage", 73);
            buildings.Add("Frame House", 67);
            buildings.Add("Multistory House", 89);
            buildings.Add("Clapboard House", 111);
            buildings.Add("Mansion", 188);
            buildings.Add("Brownstone House", 94);
            buildings.Add("Town House", 156);
            buildings.Add("Manor", 246);
            buildings.Add("Estate House", 123);
            buildings.Add("Apartment House", 205);
            buildings.Add("Plantation House", 311);
            buildings.Add("Arcade House", 155);
            buildings.Add("Country House", 207);
            buildings.Add("Gambrel Roof House", 259);
            buildings.Add("Urban Residence", 569);
            buildings.Add("Workers House", 285);
            buildings.Add("Boarding House", 380);
            buildings.Add("Victorian House", 474);
            return buildings;
        }

        public string Solve()
        {
            var solution = string.Empty;
            if (_currentPopulation == TargetPopulation)
                return solution;

            int i = 0;
            foreach (var building in _buildings.OrderByDescending(r => r.PopulationProvided))
            {
                var tmpRes = SolveInternal(building);
                var sum = tmpRes.Sum(r => r.PopulationProvided) + _currentPopulation;
                var res = sum == TargetPopulation ? "succeded" : "failed";
                Console.WriteLine($"{i++}: Tried for {sum} and {res}");
                if (res == "succeded")
                {
                    var strs = tmpRes.GroupBy(r => r.Name).Select(r => $"{r.Count()}x {r.Key}").ToList();
                    Console.WriteLine("With: "+ strs.Aggregate((a,b) => a + " | " + b));
                    solution = "Found!";
                }
            }
            return solution;
        }

        private List<BuildingWithInfo> SolveInternal(Building startSearchAt)
        {
            bool shouldContinue = true;
            List<BuildingWithInfo> solution = new List<BuildingWithInfo>();
            var tmpPopulation = _currentPopulation;
            List<BuildingWithInfo> lastPartialSolution = new List<BuildingWithInfo>();
            Building lastTryAdded = null;
            do
            {
                lastPartialSolution = TryAddFrom(tmpPopulation, startSearchAt);
                solution.AddRange(lastPartialSolution);

                lastTryAdded = lastPartialSolution.FirstOrDefault();
                if(lastPartialSolution.Any())
                    tmpPopulation += lastPartialSolution.Sum(r => r.PopulationProvided);

                Building next;
                if (lastPartialSolution.Any() && TryGetNext(lastTryAdded, tmpPopulation, out next))
                {
                    startSearchAt = next;
                }
                else
                {
                    shouldContinue = false;
                }
            } while (shouldContinue);
            return solution;
        }


        private bool TryGetNext(Building lastTryAdded,int seed, out Building next)
        {
            next = null;
            var ordered = _buildings.OrderByDescending(r => r.PopulationProvided).ToList();
            if (lastTryAdded != null)
                ordered = ordered.Where(r => r.PopulationProvided <= lastTryAdded.PopulationProvided).ToList();
            next = ordered.FirstOrDefault(r => r.PopulationProvided + seed <= TargetPopulation);
            return next != null;
        }

        private List<BuildingWithInfo> TryAddFrom(int seed, Building startSearchAt)
        {
            List<BuildingWithInfo> ret = new List<BuildingWithInfo>();
            var remaining = TargetPopulation - seed; //997
            var buildingsThatDoNotOverflow = _buildings
                .Where(r => r.PopulationProvided <= remaining)
                .Where(r => r.PopulationProvided <= startSearchAt.PopulationProvided)
                .OrderByDescending(r => r.PopulationProvided)
                .ToList();
            var anyMultiples = buildingsThatDoNotOverflow.Where(r => remaining % r.PopulationProvided == 0).ToList();

            if (anyMultiples.Any())
            {
                var multiple = anyMultiples.First();
                var howMany = remaining/multiple.PopulationProvided;
                for (int i = 0; i < howMany;i++)
                {
                    ret.Add(new BuildingWithInfo() {Action = "Add", Name = multiple.Name, PopulationProvided = multiple.PopulationProvided});
                }
                return ret;
            }
            if (buildingsThatDoNotOverflow.Any())
            {
                var tmp = buildingsThatDoNotOverflow.First();
                ret.Add(new BuildingWithInfo()
                {
                    Action = "Add",
                    Name = tmp.Name,
                    PopulationProvided = tmp.PopulationProvided
                });
            }
            return ret;

        }
    }
}