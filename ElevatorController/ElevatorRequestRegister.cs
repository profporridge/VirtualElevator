using System;
using System.Collections.Generic;
using System.Text;
using Models;
namespace ElevatorController
{
    public class ElevatorRequestRegister
    {
        private Direction[] _floorCollection;

        // this class contains the current request state for each floor 
        // and is used to determine if the elevator should stop at a given floor
        public ElevatorRequestRegister(int numberOfFloors)
        {
            _floorCollection = new Direction[numberOfFloors];
            NumberOfFloors = numberOfFloors;
        }
        public int NumberOfFloors { get; private set; }

        public ElevatorRequestRegister SetFloor(Floor floor, Direction direction)
        {
            if (floor.Id >= NumberOfFloors || (floor.Id < 0))
            {
                throw new InvalidOperationException("Floor Out Of Range");
            }
            _floorCollection[floor.Id] = direction;
            return this;
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
