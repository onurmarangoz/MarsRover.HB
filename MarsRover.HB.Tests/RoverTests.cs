using MarsRover.HB.App.Entities;
using MarsRover.HB.App.Entities.Interfaces;
using MarsRover.HB.App.Enumerations;
using MarsRover.HB.Shared.Enumerations;
using Xunit;

namespace MarsRover.HB.Tests
{
    public class RoverTests
    {
        private IRover rover;

        [Theory]
        [InlineData(0, Directions.N)]
        public void CalculateDirection_zeroValue_ReturnSuccess(int direction, Directions expectedDirection)
        {
            Coordinate coordinate = new Coordinate(1, 2);
            this.rover = new Rover(coordinate, (Directions)direction);
            this.rover.CalculateDirection();

            Assert.Equal(expectedDirection, rover.direction);
        }

        [Theory]
        [InlineData(1, Directions.E)]
        [InlineData(2, Directions.S)]
        [InlineData(3, Directions.W)]
        [InlineData(100, Directions.N)]
        [InlineData(101, Directions.E)]
        [InlineData(102, Directions.S)]
        [InlineData(103, Directions.W)]
        public void CalculateDirection_simpleValue_ReturnSuccess(int direction, Directions expectedDirection)
        {
            Coordinate coordinate = new Coordinate(1, 2);
            this.rover = new Rover(coordinate, (Directions)direction);
            this.rover.CalculateDirection();

            Assert.Equal(expectedDirection, rover.direction);
        }

        [Theory]
        [InlineData(-1, Directions.W)]
        [InlineData(-2, Directions.S)]
        [InlineData(-3, Directions.E)]
        [InlineData(-4, Directions.N)]
        [InlineData(-101, Directions.W)]
        [InlineData(-102, Directions.S)]
        [InlineData(-103, Directions.E)]
        [InlineData(-104, Directions.N)]
        public void CalculateDirection_negativeValue_ReturnSuccess(int direction, Directions expectedDirection)
        {
            Coordinate coordinate = new Coordinate(1, 2);
            this.rover = new Rover(coordinate, (Directions)direction);
            this.rover.CalculateDirection();

            Assert.Equal(expectedDirection, rover.direction);
        }

        [Theory]
        [InlineData(1, 2, Directions.N, Axis.Y)]
        [InlineData(1, 2, Directions.S, Axis.Y)]
        [InlineData(1, 2, Directions.E, Axis.X)]
        [InlineData(1, 2, Directions.W, Axis.X)]
        public void FindAxis_simpleValue_ReturnAxis(int x, int y, Directions direction, Axis expectedAxis)
        {
            Coordinate coordinate = new Coordinate(1, 2);
            this.rover = new Rover(coordinate, direction);
            this.rover.FindAxis(out Axis axis);

            Assert.Equal(expectedAxis, axis);
        }

        [Theory]
        [InlineData(1, 2, Directions.N, 1)]
        [InlineData(1, 2, Directions.S, -1)]
        [InlineData(1, 2, Directions.E, 1)]
        [InlineData(1, 2, Directions.W, -1)]
        public void FindUnitMoveForAxis_simpleValue_ReturnAxis(int x, int y, Directions direction, int expectedUnitMove)
        {
            Coordinate coordinate = new Coordinate(1, 2);
            this.rover = new Rover(coordinate, direction);
            this.rover.FindUnitMoveForAxis(out int unitMove);

            Assert.Equal(expectedUnitMove, unitMove);
        }

        [Theory]
        [InlineData(1, 2, Directions.N, Axis.X, 1, 2 , 2)]
        [InlineData(1, 2, Directions.S, Axis.X, -1, 0, 2)]
        public void Move_simpleValue_ReturnAxis(int x, int y, Directions direction, Axis axis, int unitMove, int expectedX, int expectedY)
        {
            Coordinate coordinate = new Coordinate(x, y);
            this.rover = new Rover(coordinate, (Directions)direction);
            this.rover.Move(axis, unitMove);
            Assert.Equal(expectedX, rover.coordinate.x);
            Assert.Equal(expectedY, rover.coordinate.y);
        }

        [Theory]
        [InlineData(1, 2, Directions.N, "1 2 N")]
        [InlineData(5, 9, Directions.S, "5 9 S")]
        public void ToInstantLocation_simpleValue_ReturnLocation(int x, int y, Directions direction, string expectedLocation)
        {
            Coordinate coordinate = new Coordinate(x, y);
            this.rover = new Rover(coordinate, (Directions)direction);
            this.rover.ToInstantLocation();
            Assert.Equal(expectedLocation, this.rover.ToInstantLocation());
            
        }
    }
}
