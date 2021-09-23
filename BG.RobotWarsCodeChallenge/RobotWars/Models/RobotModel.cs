using RobotWars.Common.Enums;

namespace RobotWars.Models
{
    public class RobotModel
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public Cardinal CardinalDirection { get; set; }
    }
}