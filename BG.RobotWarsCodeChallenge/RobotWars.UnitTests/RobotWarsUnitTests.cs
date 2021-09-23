using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotWars.Common.Enums;

namespace RobotWars.UnitTests
{
    [TestClass]
    public class RobotWarsUnitTests
    {
        private readonly ArenaController _arenaController;

        public RobotWarsUnitTests()
        {
            _arenaController = new ArenaController();
            _arenaController.AddRobots(1, 1, 2, "N");
            _arenaController.AddRobots(2, 3, 3, "E");
            _arenaController.PlaceRobotInStartPosition();
        }

        #region [ Battle Arena Setup ]

        [TestMethod]
        public void CreatedArena_IsNotNull()
        {
            Assert.IsNotNull(_arenaController);
        }

        [TestMethod]
        public void CreatedArena_Has_36_Cells()
        {
            int cellCount = _arenaController.Cells.Length;

            Assert.AreEqual(36, cellCount);
        }

        [TestMethod]
        public void CreatedArena_Has_2_Players()
        {
            int robotCount = _arenaController.Robots.Count;

            Assert.AreEqual(2, robotCount);
        }

        [TestMethod]
        public void Robot1_Starts_in_Cell_1_2()
        {
            int row = _arenaController.Robots[0].RowNumber;
            int col = _arenaController.Robots[0].ColumnNumber;
            int actual = _arenaController.Cells[row, col];

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Robot2_Starts_in_Cell_3_3()
        {
            int row = _arenaController.Robots[1].RowNumber;
            int col = _arenaController.Robots[1].ColumnNumber;
            int actual = _arenaController.Cells[row, col];

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Robot1_Starts_in_Cardinal_North()
        {
            Cardinal actualCardinal = _arenaController.Robots[0].CardinalDirection;

            Assert.AreEqual(Cardinal.North, actualCardinal);
        }

        [TestMethod]
        public void Robot2_Starts_in_Cardinal_East()
        {
            Cardinal actualCardinal = _arenaController.Robots[1].CardinalDirection;

            Assert.AreEqual(Cardinal.East, actualCardinal);
        }

        #endregion [ Battle Arena Setup ]

        #region [ Input Validation ]

        [TestMethod]
        public void InputNumeric_Gives_NoNumeric_Error()
        {
            const string input = "1";
            string actual = _arenaController.ValidateInput(input);

            Assert.AreEqual("Input Cannot Contain Numerics", actual);
        }

        [TestMethod]
        public void InputIsEmptyOrNull_Gives_NoNullorEmpty_Error()
        {
            string input = string.Empty;
            string actual = _arenaController.ValidateInput(input);

            Assert.AreEqual("Input Cannot Be Empty or Null", actual);
        }

        [TestMethod]
        public void InputMatches_Valid_Values()
        {
            foreach (string input in new string[] { "M", "L", "R" })
            {
                string actual = _arenaController.ValidateInput(input.ToUpper());
                Assert.AreEqual("Input Is Valid", actual);
            }
        }

        #endregion [ Input Validation ]

        #region [ Robot Movement ]

        [TestMethod]
        public void NewCardinalPosition_IsOldCardinalPosition_PlusDirection()
        {
            #region [ Turn Left ]

            Cardinal oldDirection = Cardinal.North;
            Cardinal newDirection = _arenaController.TurnRobot("L", oldDirection);
            Assert.AreEqual(Cardinal.West, newDirection);

            oldDirection = Cardinal.West;
            newDirection = _arenaController.TurnRobot("L", oldDirection);
            Assert.AreEqual(Cardinal.South, newDirection);

            oldDirection = Cardinal.South;
            newDirection = _arenaController.TurnRobot("L", oldDirection);
            Assert.AreEqual(Cardinal.East, newDirection);

            oldDirection = Cardinal.East;
            newDirection = _arenaController.TurnRobot("L", oldDirection);
            Assert.AreEqual(Cardinal.North, newDirection);

            #endregion [ Turn Left ]

            #region [ Turn Right ]

            oldDirection = Cardinal.North;
            newDirection = _arenaController.TurnRobot("R", oldDirection);
            Assert.AreEqual(Cardinal.East, newDirection);

            oldDirection = Cardinal.East;
            newDirection = _arenaController.TurnRobot("R", oldDirection);
            Assert.AreEqual(Cardinal.South, newDirection);

            oldDirection = Cardinal.South;
            newDirection = _arenaController.TurnRobot("R", oldDirection);
            Assert.AreEqual(Cardinal.West, newDirection);

            oldDirection = Cardinal.West;
            newDirection = _arenaController.TurnRobot("R", oldDirection);
            Assert.AreEqual(Cardinal.North, newDirection);

            #endregion [ Turn Right ]
        }

        [TestMethod]
        public void MoveRobotIntoCell()
        {
            _arenaController.MoveRobot(_arenaController.Robots[0].Id);
            int finishCell = _arenaController.Cells[1, 3];

            Assert.AreEqual(1, finishCell);
        }

        [TestMethod]
        public void SendAllInputs_To_Robot1_and_Return_FinishPlace()
        {
            var inputs = new string[] { "L", "M", "L", "M", "L", "M", "L", "M", "M" };
            _arenaController.SendInputsToRobot(_arenaController.Robots[0].Id, inputs);
            var finishCell = _arenaController.Cells[1, 3];

            Assert.AreEqual(1, finishCell);
        }

        [TestMethod]
        public void SendAllInputs_To_Robot2_and_Return_FinishPlace()
        {
            var inputs = new string[] { "M", "M", "R", "M", "M", "R", "M", "R", "R", "M" };
            _arenaController.SendInputsToRobot(_arenaController.Robots[1].Id, inputs);
            var finishCell = _arenaController.Cells[5, 1];

            Assert.AreEqual(2, finishCell);
        }

        [TestMethod]
        public void ValidateFinishCardinal_Robot1_Is_North()
        {
            var inputs = new string[] { "L", "M", "L", "M", "L", "M", "L", "M", "M" };
            _arenaController.SendInputsToRobot(_arenaController.Robots[0].Id, inputs);
            Cardinal actualDirection = _arenaController.Robots[0].CardinalDirection;

            Assert.AreEqual(Cardinal.North, actualDirection);
        }

        [TestMethod]
        public void ValidateFinishCardinal_Robot2_Is_East()
        {
            var inputs = new string[] { "M", "M", "R", "M", "M", "R", "M", "R", "R", "M" };
            _arenaController.SendInputsToRobot(_arenaController.Robots[1].Id, inputs);
            Cardinal actualDirection = _arenaController.Robots[1].CardinalDirection;

            Assert.AreEqual(Cardinal.East, actualDirection);
        }

        #endregion [ Robot Movement ]
    }
}