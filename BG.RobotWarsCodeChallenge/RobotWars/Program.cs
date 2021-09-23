﻿using RobotWars.Common.Helpers;
using System;

namespace RobotWars
{
    internal class Program
    {
        private static void Main()
        {
            Console.Write("Enter Grid Size: ");
            var gridSize = Console.ReadLine();
            var removedWhiteSpace = InputValidate.RemoveWhiteSpace(gridSize);
            var isNumber = InputValidate.ValidateNumeric(removedWhiteSpace);

            if (!isNumber)
            {
                Console.WriteLine("Grid Size Is Not In Numeric Format");
                Environment.Exit(0);
            }
            else
            {
                #region [ Set Up Arena ]
                var cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);
                int row = Convert.ToInt32(cleansedInput[0]);
                int column = Convert.ToInt32(cleansedInput[1]);

                ArenaController arena = new ArenaController(row + 1, column + 1);
                #endregion

                #region [ Setup Robot 1 Starting Cell]
                Console.Write("Enter Robot 1 Start Position and Cardinal Direction: ");
                var inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                if (InputValidate.ValidateNumeric(cleansedInput[0]))
                {
                    if (InputValidate.ValidateNumeric(cleansedInput[1]))
                    {
                        arena.AddRobots(1, Convert.ToInt32(cleansedInput[0]), Convert.ToInt32(cleansedInput[1]), cleansedInput[2].ToUpper());
                    }
                }
                #endregion

                #region [ Setup Robot 2 Starting Cell ]
                Console.Write("Enter Robot 2 Start Position and Cardinal Direction: ");
                inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                if (InputValidate.ValidateNumeric(cleansedInput[0]))
                {
                    if (InputValidate.ValidateNumeric(cleansedInput[1]))
                    {
                        arena.AddRobots(2, Convert.ToInt32(cleansedInput[0]), Convert.ToInt32(cleansedInput[1]), cleansedInput[2].ToUpper());
                    }
                }
                #endregion

                arena.PlaceRobotInStartPosition();

                #region [ Setup and Send Robot1 Movements ]
                Console.Write("Enter Robot 1 Movements: ");
                inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                arena.SendInputsToRobot(1, cleansedInput);
                #endregion

                #region [ Setup and Send Robot2 Movements ]
                Console.Write("Enter Robot 2 Movements: ");
                inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                arena.SendInputsToRobot(2, cleansedInput);
                #endregion


                foreach (var item in arena.Robots)
                {
                    Console.WriteLine($"{item.RowNumber} {item.ColumnNumber} {item.CardinalDirection}");
                }
            }
        }
    }
}