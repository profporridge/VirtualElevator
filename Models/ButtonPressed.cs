using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ButtonPressed : IEvent
    {
        public int SourceFloor { get; set; }
        public int DestFloor { get; set; } = -1;
        public Direction UpDirection { get; set; }
    }
    public class ElevatorDispatched :IEvent
    {
        public Floor Floor { get; set; }
        public Direction Direction { get; set; }
    }

    public class ElevatorArrivedAtFloor : IEvent
    {
        public Floor Floor { get; set; }
    }
    public class ApproachingFloor : IEvent
    {
        public Floor NextFloor { get; set; }
        public Direction Direction { get; set; }
    }
}
