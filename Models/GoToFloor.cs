using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GoToFloor : ICommand
    {
        public Floor Floor { get; set; }
        public Direction Direction {get;set;}
    }
    public class MoveElevator : ICommand
    {
        public Direction Direction { get; set; }
    }
}
