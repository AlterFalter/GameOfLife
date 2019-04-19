using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysGameOfLifeTest
{
    [TestClass]
    public class GameOfLifeTest
    {
        public GameOfLife Game { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Game = new GameOfLife();
        }

        [TestMethod]
        public void BoolToIntTest()
        {
            Assert.AreEqual(GameOfLife.BoolToInt(true), 1);
            Assert.AreEqual(GameOfLife.BoolToInt(false), 0);
        }

        [TestMethod]
        public void IsAliveAndIsNeighbourAliveTest()
        {
            Assert.IsFalse(Game.IsAlive(1, 1) && Game.IsNeighbourAlive(0, 0, 1, 1));

            Game.SetLife(1, 1, true);

            Assert.IsTrue(Game.IsAlive(1, 1) && Game.IsNeighbourAlive(0, 0, 1, 1));

            Game.SetLife(1, 1, false);

            Assert.IsFalse(Game.IsAlive(1, 1) && Game.IsNeighbourAlive(0, 0, 1, 1));

            Assert.IsFalse(Game.IsNeighbourAlive(0, 0, -1, 0));
            Assert.IsFalse(Game.IsNeighbourAlive(0, 0, 0, -1));
        }

        [TestMethod]
        public void CheckCellTest()
        {
            Assert.IsFalse(Game.IsAlive(1, 1));
            Assert.IsFalse(Game.IsAlive(2, 1));
            Assert.IsFalse(Game.IsAlive(3, 1));
            Assert.IsFalse(Game.IsAlive(1, 2));
            Assert.IsFalse(Game.IsAlive(2, 2));
            Assert.IsFalse(Game.IsAlive(3, 2));
            Assert.IsFalse(Game.IsAlive(1, 3));
            Assert.IsFalse(Game.IsAlive(2, 3));
            Assert.IsFalse(Game.IsAlive(3, 3));

            Game.SetLife(2, 2, true);

            Assert.IsTrue(Game.IsAlive(2, 2));

            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 0, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 1, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, 0));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 1, 0));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, 1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 0, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 1, 1));

            Game.SetLife(3, 2, true);
            Assert.IsTrue(Game.IsNeighbourAlive(2, 2, 1, 0));
            Assert.IsTrue(Game.IsNeighbourAlive(3, 2, -1, 0));

            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 0, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 1, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, 0));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, -1, 1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 0, -1));
            Assert.IsFalse(Game.IsNeighbourAlive(2, 2, 1, 1));
        }
    }
}
