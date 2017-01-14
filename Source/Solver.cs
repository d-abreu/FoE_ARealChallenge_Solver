using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgeOfEmpiresARealChallengeSolver
{
    internal class Solver
    {
        private readonly int _currentPopulation;
        private readonly Buildings _buildings;
        private const int TargetPopulation = 1997;
        public const string NothingToBeDone = "Nothing to be done!";
        public const string Impossible = "Impossible!";

        public Solver(int currentPopulation, Buildings buildings)
        {
            _currentPopulation = currentPopulation;
            _buildings = buildings;
        }

        public string Solve()
        {
            StringBuilder sb = new StringBuilder();
            if (_currentPopulation == TargetPopulation)
                return NothingToBeDone;

            foreach (var building in _buildings.OrderByDescending(r => r.PopulationProvided))
            {
                var tmpRes = SolveInternal(building);
                var sum = tmpRes.Sum(r => r.PopulationProvided) + _currentPopulation;
                if (sum == TargetPopulation)
                {
                    var strs = tmpRes.GroupBy(r => r.Name).Select(r => $"{r.Count()}x {r.Key}").ToList();
                    sb.AppendLine(strs.Aggregate((a, b) => a + " | " + b));
                }
            }
            var ret = sb.ToString();
            Console.WriteLine(ret);
            return ret;
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