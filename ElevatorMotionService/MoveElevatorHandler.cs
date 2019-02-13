using Models;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorMotionService
{
    class MoveElevatorHandler : IHandleMessages<MoveElevator>
    {
        static ILog log = LogManager.GetLogger<MoveElevatorHandler>();
        public Task Handle(MoveElevator message, IMessageHandlerContext context)
        {
            Task.Delay(2000);
            var elevatorMotion = new ElevatorMotion { Direction = message.Direction };
            return context.Publish(elevatorMotion);
        }
    }
}
