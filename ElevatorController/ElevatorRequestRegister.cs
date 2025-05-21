using System;
using System.Collections.Generic;
using System.Text;
using Models;
namespace ElevatorController
{
   
    public class DirectedRegister
    {
        public DirectedRegister(int numberOfFloors)
        {
            Up = new bool[numberOfFloors];
            Down = new bool[numberOfFloors];
        }
        public bool[] Up { get; set; }
        public bool[] Down { get; set; }
        public Direction this[int index]
        {
            get
            {
                if (Up[index])
                    return Direction.Up;
                if (Down[index])
                    return Direction.Down;
                return Direction.None;
            }
            set
            {
                Up[index] = (value == Direction.Up);
                Down[index] = (value == Direction.Down);
            }
        }
    }
    public class ElevatorRequestRegister
    {
        private DirectedRegister _floorCollection;

        // this class contains the current request state for each floor 
        // and is used to determine if the elevator should stop at a given floor
        public ElevatorRequestRegister(int numberOfFloors)
        {
            _floorCollection = new DirectedRegister(numberOfFloors);
            NumberOfFloors = numberOfFloors;
        }
        public int NumberOfFloors { get; private set; }

        public bool SetFloor(Floor floor, Direction direction)
        {
            if (floor.Id >= NumberOfFloors || (floor.Id < 0))
            {
                throw new InvalidOperationException("Floor Out Of Range");
            }
            if (_floorCollection[floor.Id] == direction)
            {
                // already set to this direction
                return false;
            }
            _floorCollection[floor.Id] = direction;

            return true ;
        }
        public bool CheckFloor(Floor floor, Direction direction)
        {
            return CheckFloor(floor.Id, direction);
        }

        private bool CheckFloor(int id, Direction direction)
        {
            var returnVar = _floorCollection[id];
            return (direction == returnVar);
        }
        public void ClearFloor(Floor floor, Direction direction) {
            var state = _floorCollection[floor.Id];
            if (state == direction)
                _floorCollection[floor.Id] = Direction.None;
        }
    }
}
