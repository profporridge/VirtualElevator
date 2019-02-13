using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ButtonPressed : IEvent
    {
        public int Floor { get; set; }
        public bool UpDirection { get; set; }
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
