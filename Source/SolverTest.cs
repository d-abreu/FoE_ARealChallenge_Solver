using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ForgeOfEmpiresARealChallengeSolver
{
    [TestFixture]
    internal class SolverTest
    {
        [Test]
        public void GivenPopulationIsAlready1997_WhenSolvingFor1997_ThenReturnEmpty()
        {
            var solver = new Solver(1997, BuildingsFactory.GetAllBuildings());
            var ret = solver.Solve();

            Assert.AreEqual(Solver.NothingToBeDone, ret);
        }

        [Test]
        public void GivenPopulationIs1996_WhenSolvingFor1997_ThenIsImpossible()
        {
            var solver = new Solver(1996, BuildingsFactory.GetAllNonPremiumBuildings());
            var ret = solver.Solve();

            Assert.AreEqual(Solver.Impossible, ret);
        }

        [Test]
        public void GivenPopulationIs1859_WhenSolvingFor1997_ThenFindSolutions()
        {
            var solver = new Solver(1859, BuildingsFactory.GetAllNonPremiumBuildings());
            var ret = solver.Solve();

            AssertSolutionCount(ret,5);
        }

        [Test]
        public void GivenPopulationIs1970_WhenSolvingFor1997_ThenFindSolutions()
        {
            var solver = new Solver(1970, BuildingsFactory.GetAllNonPremiumBuildings());
            var ret = solver.Solve();

            AssertSolutionCount(ret, 1);
            Assert.AreEqual("1x Thatched House", ret);
        }

        [Test]
        public void GivenPopulationIs1000_WhenSolvingFor1997_ThenFindSolutions()
        {
            var solver = new Solver(1000, BuildingsFactory.GetAllNonPremiumBuildings());
            var ret = solver.Solve();

            AssertSolutionCount(ret, 11);
        }

        private static void AssertSolutionCount(string solution, int expectedCount)
        {
            if (solution == null)
                solution = string.Empty;
            Assert.AreEqual(expectedCount,solution.Split(new [] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries).Count());
        }
    }
}
