using MarsRover.HB.App.Constants;
using MarsRover.HB.App.Entities;
using MarsRover.HB.App.Entities.Interfaces;
using MarsRover.HB.App.Enumerations;
using MarsRover.HB.App.Helpers;
using MarsRover.HB.App.Services.Interfaces;
using MarsRover.HB.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.HB.App.Services
{
    public class RoverService : IRoverService
    {
        private Coordinate coordinate { get; set; }
        private List<string> roverLocationList { get; set; }

        public RoverService()
        {
            roverLocationList = new List<string>();
        }

        /// <summary>
        /// Kullanıcı tarafından girilen dataları validate eder.
        /// Nesnelerini oluşturur, plato keşif işlemini tetikler.
        /// </summary>
        /// <param name="plateauString"></param>
        /// <param name="roverAString"></param>
        /// <param name="roverAMapPath"></param>
        /// <param name="roverBString"></param>
        /// <param name="roverBMapPath"></param>
        /// <returns></returns>
        public List<string> RoverProcess(string plateauString, string roverAString, string roverAMapPath, string roverBString, string roverBMapPath)
        {
            #region General Validation And Create Operations

            Plateau plateau = ValidateAndCreateByPlateauString(plateauString);
            Rover roverA = ValidateAndCreateByRoverString(roverAString, plateau.coordinate);
            Rover roverB = ValidateAndCreateByRoverString(roverBString, plateau.coordinate);
            ValidateMapPath(roverAMapPath);
            ValidateMapPath(roverBMapPath);

            #endregion

            #region Explore to Plateau Operations

            ExploreToPlateau(plateau, roverA, roverAMapPath);
            roverLocationList.Add(roverA.ToInstantLocation());

            ExploreToPlateau(plateau, roverB, roverBMapPath);
            roverLocationList.Add(roverB.ToInstantLocation());

            #endregion

            return roverLocationList;
        }

        /// <summary>
        /// İlgili Rover ile Platoyu keşfeder.
        /// Move ve Turn olmak üzere iki farklı işlem kırılımı gerçekleştirir.
        /// </summary>
        /// <param name="plateau"></param>
        /// <param name="rover"></param>
        /// <param name="mapPath"></param>
        private void ExploreToPlateau(Plateau plateau, Rover rover, string mapPath)
        {
            foreach (var mapItem in mapPath.ToArray())
            {
                if (mapItem == Movements.Move)
                    MoveProcess(rover, plateau);
                else
                    TurnProcess(rover, mapItem);
            }
        }

        /// <summary>
        /// Hareket süreci koordinat düzlemi üzerinden hesaplanmıştır.
        /// Hareket edilecek ekseni ve birim hareket miktarını bulur.
        /// Platodan snır kontrolu ve diğer araçlar ile çarpışma durumunu kontrol eder.
        /// Ve Hareket eder.
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="plateau"></param>
        private void MoveProcess(Rover rover, Plateau plateau)
        {
            rover.FindAxis(out Axis axis);
            rover.FindUnitMoveForAxis(out int unitMove);
            plateau.coordinate.CheckBorder(axis, rover.coordinate, unitMove, out int tempCoordinateX, out int tempCoordinateY);
            CheckCrash(tempCoordinateX + " " + tempCoordinateY + " " + rover.direction);
            rover.Move(axis, unitMove);
        }

        /// <summary>
        /// Dönüşler saat yönünde artan tersi yönde azalan şekilde hesaplanmıştır.
        /// Verilen yön sayısına göre mod alınma işlemi yapılarak bulunur
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="mapItem"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void TurnProcess(Rover rover, char mapItem)
        {
            FindUnitMoveForDirection(mapItem, out int unitMove);
            rover.Turn(unitMove);
        }

        /// <summary>
        /// Çarpışma durumunu kontrol eder
        /// </summary>
        /// <param name="tempCoordinate"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void CheckCrash(string tempCoordinate)
        {
            if (roverLocationList.Where(x => x == tempCoordinate).Count() > 0)
                throw new ArgumentOutOfRangeException(ErrorCodes.CrashException);
        }

        /// <summary>
        /// Yön için değişecek birim hareket miktarını bulur
        /// </summary>
        /// <param name="mapItem"></param>
        /// <param name="unitMove"></param>
        /// <returns></returns>
        private void FindUnitMoveForDirection(char mapItem, out int unitMove)
        {
            RoverDictionaryHelper.movementToUnitMoveDictionary.TryGetValue(mapItem, out unitMove);
        }

        /// <summary>
        /// Ham Plato datasını kontrol eder ve Plato nesnesi oluşturur.
        /// </summary>
        /// <param name="plateauString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        private Plateau ValidateAndCreateByPlateauString(string plateauString)
        {
            if (string.IsNullOrEmpty(plateauString))
                throw new ArgumentNullException(ErrorCodes.NullReferenceExceptionForPleteau);

            var plateauList = plateauString.Split(" ");

            if (plateauList.Count() != 2)
                throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeExceptionForPleteau);

            bool isValidX = int.TryParse(plateauList[0], out int plateauX);
            bool isValidY = int.TryParse(plateauList[1], out int plateauY);

            if (!isValidX || !isValidY)
                throw new FormatException(ErrorCodes.FormatExceptionForPleteau);

            if (plateauX < 0 || plateauY < 0)
                throw new ArgumentOutOfRangeException(ErrorCodes.NegativeExceptionForPleteau);

            this.coordinate = new Coordinate(plateauX, plateauY);
            return new Plateau(this.coordinate);
        }

        /// <summary>
        /// Ham Rover datasını kontrol eder ve Rover nesnesi oluşturur.
        /// </summary>
        /// <param name="roverString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        private Rover ValidateAndCreateByRoverString(string roverString, ICoordinate plateauCoordinate)
        {
            if (string.IsNullOrEmpty(roverString))
                throw new ArgumentNullException(ErrorCodes.NullReferenceExceptionForRover);

            var roverList = roverString.Split(" ");

            if (roverList.Count() != 3)
                throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeExceptionForRover);

            bool isValidX = int.TryParse(roverList[0], out int roverX);
            bool isValidY = int.TryParse(roverList[1], out int roverY);

            if (!isValidX || !isValidY)
                throw new FormatException(ErrorCodes.FormatExceptionForRover);

            if (roverX < 0 || roverY < 0)
                throw new ArgumentOutOfRangeException(ErrorCodes.NegativeExceptionForRover);

            bool isValidDirection = Enum.TryParse(typeof(Directions), roverList[2].ToString(), out var direction);

            if (!isValidDirection)
                throw new FormatException(ErrorCodes.InvalidDirectionExceptionForRover);

            if (plateauCoordinate.x < roverX)
                throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForXCoordinateException);

            if (plateauCoordinate.y < roverY)
                throw new ArgumentOutOfRangeException(ErrorCodes.OutOfRangeForYCoordinateException);

            this.coordinate = new Coordinate(roverX, roverY);
            return new Rover(this.coordinate, (Directions)direction);
        }

        /// <summary>
        /// Ham Harita datasını kontrol eder.
        /// </summary>
        /// <param name="mapPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ValidateMapPath(string mapPath)
        {
            if (string.IsNullOrEmpty(mapPath))
                throw new ArgumentNullException(ErrorCodes.NullReferenceExceptionForMapPath);

            var mapPathList = mapPath.ToCharArray().Where(x => x != Movements.Left && x != Movements.Right && x != Movements.Move);

            if (mapPathList.Count() != 0)
                throw new ArgumentOutOfRangeException(ErrorCodes.InvalidDirectionExceptionForMapItem);
        }
    }
}
