using MarsRover.HB.App.Entities.Interfaces;
using MarsRover.HB.App.Enumerations;
using MarsRover.HB.App.Helpers;
using MarsRover.HB.Shared.Enumerations;
using System;

namespace MarsRover.HB.App.Entities
{
    public class Rover : IRover
    {
        public ICoordinate coordinate { get; set; }
        public Directions direction { get; set; }

        public Rover() {}

        public Rover(Coordinate coordinate, Directions directions)
        {
            this.coordinate = coordinate;
            this.direction = directions;
        } 
        
        public void Turn(int unitMove)
        {
            this.direction += unitMove;
            CalculateDirection();
        }

        public void CalculateDirection()
        {
            var lenghtOfDirections = Enum.GetValues(typeof(Directions)).Length;
            this.direction = FindDirection(lenghtOfDirections);
        }
        private Directions FindDirection(int lenghtOfDirections)
        {
            var numericalDirection = (int)this.direction % lenghtOfDirections;

            if (numericalDirection < 0)
                numericalDirection = numericalDirection + lenghtOfDirections;

            return (Directions)numericalDirection;
        }

        public void FindAxis(out Axis axis)
        {
            RoverDictionaryHelper.directionToAxisDictionary.TryGetValue(this.direction, out axis);
        }

        public void FindUnitMoveForAxis(out int unitMove)
        {
            RoverDictionaryHelper.directionToUnitMoveDictionary.TryGetValue(this.direction, out unitMove);
        }

        public void Move(Axis axis, int unitMove)
        {
            if (axis == Axis.X)
                this.coordinate.x += unitMove;
            else
                this.coordinate.y += unitMove;
        }

        public string ToInstantLocation()
        {
            return String.Format("{0} {1} {2}", this.coordinate.x.ToString(), this.coordinate.y.ToString(), this.direction);
        }
    }
}
