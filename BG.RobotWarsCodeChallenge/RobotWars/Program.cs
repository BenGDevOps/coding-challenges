using RobotWars.Common.Helpers;
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
                var cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);
                int row = Convert.ToInt32(cleansedInput[0]);
                int column = Convert.ToInt32(cleansedInput[1]);

                ArenaController arena = new ArenaController(row + 1, column + 1);
                arena.AddRobots();
                arena.PlaceRobotInStartPosition();

                Console.Write("Enter Robot 1 Movements: ");
                var inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                arena.SendInputsToRobot(1, cleansedInput);
                Console.Write("Enter Robot 2 Movements: ");
                inputs = Console.ReadLine();
                removedWhiteSpace = InputValidate.RemoveWhiteSpace(inputs);
                cleansedInput = InputValidate.CleanseInput(removedWhiteSpace);

                arena.SendInputsToRobot(2, cleansedInput);

                foreach (var item in arena.Robots)
                {
                    Console.WriteLine($"{item.RowNumber} {item.ColumnNumber} {item.CardinalDirection}");
                }
            }
        }
    }
}