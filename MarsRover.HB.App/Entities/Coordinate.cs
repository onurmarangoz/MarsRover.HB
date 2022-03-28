using MarsRover.HB.App.Constants;
using MarsRover.HB.App.Entities.Interfaces;
using MarsRover.HB.Shared.Enumerations;
using System;

namespace MarsRover.HB.App.Entities
{
    public class Coordinate : ICoordinate
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void CheckBorder(Axis axis, ICoordinate coordinate, int unitMove, out int tempCoordinateX, out int tempCoordinateY)
        {
            int coordinateValue = unitMove;
            tempCoordinateX = coordinate.x;
            tempCoordinateY = coordinate.y;

            if (axis == Axis.X)
            {
                coordinateValue += coordinate.x;

                if (coordinateValue < 0 || coordinateValue > this.x)
                    throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForXCoordinateException);

                tempCoordinateX = coordinateValue;
            }
            else
            {
                coordinateValue += coordinate.y;

                if (coordinateValue < 0 || coordinateValue > this.y)
                    throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForYCoordinateException);

                tempCoordinateY= coordinateValue;
            }
        }
    }
}
