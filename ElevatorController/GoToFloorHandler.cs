using Models;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorController
{
    public class GoToFloorHandler : IHandleMessages<GoToFloor>
    {
        public GoToFloorHandler(ElevatorRequestRegister elevatorRequestRegister, IElevatorMotionService elevatorMotionService)
        {
            _elevatorRequestRegister = elevatorRequestRegister;
            _elevatorMotionService = elevatorMotionService;
        }
        static ILog log = LogManager.GetLogger<GoToFloorHandler>();
        private ElevatorRequestRegister _elevatorRequestRegister;
        private IElevatorMotionService _elevatorMotionService;

        public Task Handle(GoToFloor message, IMessageHandlerContext context)
        {
            log.Info($"Received GoToFloor, Floor {message.Floor}");
            _elevatorRequestRegister.SetFloor(message.Floor, message.Direction);
            
            var elevatorDispatched = new ElevatorDispatched { Floor = message.Floor, Direction = message.Direction };
            return context.Publish(elevatorDispatched);
        }
    }
}
