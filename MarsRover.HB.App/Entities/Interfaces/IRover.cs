using MarsRover.HB.App.Enumerations;
using MarsRover.HB.Shared.Enumerations;

namespace MarsRover.HB.App.Entities.Interfaces
{
    public interface IRover: ICoordinateBase
    {
        public Directions direction { get; set; }

        void CalculateDirection();
        void FindAxis(out Axis axis);
        void FindUnitMoveForAxis(out int unitMove);
        void Move(Axis axis, int unitMove);
        string ToInstantLocation();
    }
}
