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
            var solver = new Solver(1997);
            var ret = solver.Solve();

            Assert.AreEqual(string.Empty, ret);
        }

        [Test]
        public void GivenPopulationIs1000_WhenSolvingFor1997_ThenFindSolutions()
        {
            var solver = new Solver(1000);
            var ret = solver.Solve();

            Assert.IsFalse(ret == string.Empty);
        }
    }
}
