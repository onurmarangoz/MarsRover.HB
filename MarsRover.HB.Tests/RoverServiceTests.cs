using MarsRover.HB.App.Constants;
using MarsRover.HB.App.Services;
using System;
using Xunit;

namespace MarsRover.HB.Tests
{
    public class RoverServiceTests
    {
        private RoverService roverService { get; set; }

        public RoverServiceTests()
        {
            this.roverService = new RoverService();
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM", "1 3 N", "5 1 E")]
        public void RoverProcess_defaultTestValue_ReturnFinalCoordinate(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath, string expectedFinalRoverACoordinate, string expectedFinalRoverBCoordinate)
        {
            var result = this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath);
            Assert.Equal(expectedFinalRoverACoordinate, result[0]);
            Assert.Equal(expectedFinalRoverBCoordinate, result[1]);
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "LMLMLMLMMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRM", "3 3 E", "MMRMMRMRRM", "1 3 N", "5 1 E")]
        [InlineData("5 5", "1 2 N", "LMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMLMMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRMRM", "3 3 E", "MMRMMRMRRM", "1 3 N",  "5 1 E")]
        public void RoverProcess_extremeTestValue_ReturnFinalCoordinate(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath, string expectedFinalRoverACoordinate, string expectedFinalRoverBCoordinate)
        {
            var result = this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath);
            Assert.Equal(expectedFinalRoverACoordinate, result[0]);
            Assert.Equal(expectedFinalRoverBCoordinate, result[1]);
        }

        [Theory]  
        [InlineData("5 5", "3 3 E", "MMRMMRMRRM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_extremeTestValue_ReturnCrashException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.CrashException).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "MMMMMMMMMMMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 S", "MMMMMMMMMMMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_extremeTestValueForY_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForYCoordinateException).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 2 E", "MMMMMMMMMMMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 W", "MMMMMMMMMMMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_extremeTestValueForX_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForXCoordinateException).Message, exception.Message);
        }

        [Theory]
        [InlineData(null, "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_nullOrEmptyValueForPleteau_ReturnArgumentNullException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentNullException(ErrorCodes.NullReferenceExceptionForPleteau).Message, exception.Message);
        }

        [Theory]
        [InlineData("5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_outOfRangeValueForPleteau_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeExceptionForPleteau).Message, exception.Message);
        }

        [Theory]
        [InlineData("A B", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("? !", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_wrongValueForPleteau_ReturnFormatException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<FormatException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new FormatException(ErrorCodes.FormatExceptionForPleteau).Message, exception.Message);
        }

        [Theory]
        [InlineData("-1 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 -1", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("-1 -1", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_negativeValueForPleteau_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.NegativeExceptionForPleteau).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", null, "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_nullOrEmptyValueForRover_ReturnArgumentNullException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentNullException(ErrorCodes.NullReferenceExceptionForRover).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 N N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_outOfRangeValueForRover_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeExceptionForRover).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "-1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 -2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "-1 -2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_negativeValueForRover_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.NegativeExceptionForRover).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 2 K", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 ?", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 null", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_wrongDirectionValueForRover_ReturnFormatException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<FormatException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new FormatException(ErrorCodes.InvalidDirectionExceptionForRover).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "A B N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "E N N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "? ! N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_wrongValueForRover_ReturnFormatException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<FormatException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new FormatException(ErrorCodes.FormatExceptionForRover).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 2 N", null, "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 N", "", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_nullOrEmptyValueForMapPath_ReturnArgumentNullException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentNullException(ErrorCodes.NullReferenceExceptionForMapPath).Message, exception.Message);
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "LMLML MLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 N", "LMLMKGSĞLMLMM", "3 3 E", "MMRMMRMRRM")]
        [InlineData("5 5", "1 2 N", "12321321", "3 3 E", "MMRMMRMRRM")]
        public void RoverProcess_wrongDataForMapItem_ReturnArgumentOutOfRangeException(string plateauString, string roverAString, string roverMapAPath, string roverBString, string roverMapBPath)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => this.roverService.RoverProcess(plateauString, roverAString, roverMapAPath, roverBString, roverMapBPath));
            Assert.Equal(new ArgumentOutOfRangeException(ErrorCodes.InvalidDirectionExceptionForMapItem).Message, exception.Message);
        }
    }
}
