using MarsRover.HB.App.Constants;
using MarsRover.HB.App.Entities;
using MarsRover.HB.Shared.Enumerations;
using System;
using Xunit;

namespace MarsRover.HB.Tests
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData(5, 5, 1, 2, Axis.X, 0)]
        public void ValidateBorder_simpleValueForX_ReturnException(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis,int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForXCoordinateException).Message, exception.Message);
        }

        [Theory]
        [InlineData(5, 5, 1, 2, Axis.Y, 0)]
        public void ValidateBorder_simpleValueForY_ReturnException(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForYCoordinateException).Message, exception.Message);
        }


        [Theory]
        [InlineData(-1, 5, 1, 2, Axis.X, 0)]
        [InlineData(-1, 5, -3, 2, Axis.X, 0)]
        [InlineData(1, 5, -3, 2, Axis.X, 0)]
        public void ValidateBorder_negativeValueForX_ReturnException(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForXCoordinateException).Message, exception.Message);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData(null, 1, null, null)]
        [InlineData(1, 1, null, null)]
        [InlineData(null, 1, Axis.X, null)]
        [InlineData(1, null, Axis.X, null)]
        [InlineData(null, 1, Axis.Y, null)]
        [InlineData(1, null, Axis.Y, null)]
        public void ValidateBorder_nullValue_ReturnSuccess(int x, int y, Axis axis, int unitMove)
        {
            Coordinate coord = new Coordinate(x, y);
            Coordinate coordinate = new Coordinate(x, y);

            coordinate.CheckBorder(axis, coord, unitMove, out int tempCoordinateX, out int tempCoordinateY);
            Assert.True(true);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, Axis.X, 0)]
        public void ValidateBorder_zeroValueForX_ReturnSuccess(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);

            pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY);
            Assert.True(true);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, Axis.Y, 0)]
        public void ValidateBorder_zeroValueForY_ReturnSuccess(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);

            pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1, 2, 5, 5, Axis.Y, 0)]
        [InlineData(1, 2, 5, 5, Axis.X, 0)]
        public void ValidateBorder_simpleValue_ReturnSuccess(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);

            pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1, -5, 1, 2, Axis.Y, 0)]
        [InlineData(1, -5, 3, -2, Axis.Y, 0)]
        [InlineData(1, 5, 3, -2, Axis.Y, 0)]
        public void ValidateBorder_negativeValueForY_ReturnException(int roverX, int roverY, int pleteauX, int pleteauY, Axis axis, int unitMove)
        {
            Coordinate roverCoordinate = new Coordinate(roverX, roverY);
            Coordinate pleteauCoordinate = new Coordinate(pleteauX, pleteauY);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => pleteauCoordinate.CheckBorder(axis, roverCoordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForYCoordinateException).Message, exception.Message);
        }
    }
}
