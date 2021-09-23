using RobotWars.Common.Enums;
using RobotWars.Common.Helpers;
using RobotWars.Models;
using System.Collections.Generic;

namespace RobotWars
{
    public class ArenaController
    {
        public int[,] Cells { get; set; }

        public List<RobotModel> Robots { get; set; } = new List<RobotModel>();

        public ArenaController(int rows = 6, int columns = 6)
        {
            Cells = new int[rows, columns];
        }

        #region [ Arena Setup ]

        public void AddRobots()
        {
            Robots = new List<RobotModel>()
            {
                new RobotModel()
                {
                    Id = 1,
                    RowNumber = 1,
                    ColumnNumber = 2,
                    CardinalDirection = Cardinal.North
                },
                new RobotModel()
                {
                    Id = 2,
                    RowNumber = 3,
                    ColumnNumber = 3,
                    CardinalDirection = Cardinal.East
                }
            };
        }

        public void PlaceRobotInStartPosition()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    int yes = Robots.FindIndex(l => l.RowNumber == i && l.ColumnNumber == j);

                    if (yes >= 0)
                    {
                        Cells[i, j] = Robots[yes].Id;
                    }
                }
            }
        }

        #endregion [ Arena Setup ]

        #region [ Validate Inputs ]

        public string ValidateInput(string input)
        {
            return InputValidate.Validate(input);
        }

        #endregion [ Validate Inputs ]

        #region [ Robot Movements ]

        public void SendInputsToRobot(int robotId, string[] inputs)
        {
            int robotIndex = Robots.FindIndex(l => l.Id == robotId);

            foreach (string input in inputs)
            {
                if (ValidateInput(input) == "Input Is Valid")
                {
                    if (input == "M")
                    {
                        MoveRobot(robotId);
                    }
                    else
                    {
                        Robots[robotIndex].CardinalDirection = TurnRobot(input, Robots[robotIndex].CardinalDirection);
                    }
                }
            }
        }

        public Cardinal TurnRobot(string direction, Cardinal cardinal)
        {
            Cardinal result = new Cardinal();

            #region [ Left Direction ]

            if (direction == "L" && cardinal == Cardinal.North)
            {
                result = Cardinal.West;
            }

            if (direction == "L" && cardinal == Cardinal.East)
            {
                result = Cardinal.North;
            }

            if (direction == "L" && cardinal == Cardinal.South)
            {
                result = Cardinal.East;
            }

            if (direction == "L" && cardinal == Cardinal.West)
            {
                result = Cardinal.South;
            }

            #endregion [ Left Direction ]

            #region [ Right Direction ]

            if (direction == "R" && cardinal == Cardinal.North)
            {
                result = Cardinal.East;
            }

            if (direction == "R" && cardinal == Cardinal.East)
            {
                result = Cardinal.South;
            }

            if (direction == "R" && cardinal == Cardinal.South)
            {
                result = Cardinal.West;
            }

            if (direction == "R" && cardinal == Cardinal.West)
            {
                result = Cardinal.North;
            }

            #endregion [ Right Direction ]

            return result;
        }

        public void MoveRobot(int robotId)
        {
            int robotIndex = Robots.FindIndex(l => l.Id == robotId);
            int robotRow = Robots[robotIndex].RowNumber;
            int robotCol = Robots[robotIndex].ColumnNumber;

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (robotRow == i && robotCol == j)
                    {
                        Cells[i, j] = 0;

                        if (Robots[robotIndex].CardinalDirection == Cardinal.North)
                        {
                            Cells[i, j + 1] = Robots[robotIndex].Id;
                            Robots[robotIndex].RowNumber = i;
                            Robots[robotIndex].ColumnNumber = j + 1;
                        }

                        if (Robots[robotIndex].CardinalDirection == Cardinal.East)
                        {
                            Cells[i + 1, j] = Robots[robotIndex].Id;

                            Robots[robotIndex].RowNumber = i + 1;
                            Robots[robotIndex].ColumnNumber = j;
                        }

                        if (Robots[robotIndex].CardinalDirection == Cardinal.South)
                        {
                            Cells[i, j - 1] = Robots[robotIndex].Id;
                            Robots[robotIndex].RowNumber = i;
                            Robots[robotIndex].ColumnNumber = j - 1;
                        }

                        if (Robots[robotIndex].CardinalDirection == Cardinal.West)
                        {
                            Cells[i - 1, j] = Robots[robotIndex].Id;
                            Robots[robotIndex].RowNumber = i - 1;
                            Robots[robotIndex].ColumnNumber = j;
                        }
                    }
                }
            }
        }

        #endregion [ Robot Movements ]
    }
}