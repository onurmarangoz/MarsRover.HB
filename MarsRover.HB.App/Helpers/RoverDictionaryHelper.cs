using MarsRover.HB.App.Constants;
using MarsRover.HB.App.Enumerations;
using MarsRover.HB.Shared.Enumerations;

using System.Collections.Generic;

namespace MarsRover.HB.App.Helpers
{
    public static class RoverDictionaryHelper
    {
        public static Dictionary<Directions, int> directionToUnitMoveDictionary = new Dictionary<Directions, int>()
        {
            { Directions.N, 1 },
            { Directions.E, 1 },
            { Directions.S, -1 },
            { Directions.W, -1 }
        };

        public static Dictionary<Directions, Axis> directionToAxisDictionary = new Dictionary<Directions, Axis>()
        {
            { Directions.N, Axis.Y },
            { Directions.S, Axis.Y },
            { Directions.E,Axis.X },
            { Directions.W, Axis.X }
        };

        public static Dictionary<char, int> movementToUnitMoveDictionary = new Dictionary<char, int>()
        {
            { Movements.Left, -1 },
            { Movements.Right,  1 },
            { Movements.Move,  0 }
        };
    }
}
