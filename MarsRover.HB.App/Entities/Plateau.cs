using MarsRover.HB.App.Entities.Interfaces;

namespace MarsRover.HB.App.Entities
{
    public class Plateau : IPlateau, ICoordinateBase
    {
        public ICoordinate coordinate { get; set; }

        public Plateau(ICoordinate coordinate)
        {
            this.coordinate = coordinate;
        }
    }
}
