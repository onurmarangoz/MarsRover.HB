using MarsRover.HB.Shared.Enumerations;

namespace MarsRover.HB.App.Entities.Interfaces
{
    public interface ICoordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        void CheckBorder(Axis axis, ICoordinate coordinate, int unitMove, out int tempCoordinateX, out int tempCoordinateY);
    }
}
