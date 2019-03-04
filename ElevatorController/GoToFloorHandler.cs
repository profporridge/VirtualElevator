using Models;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorController
{
    

    public class GoToFloorHandler : IHandleMessages<GoToFloor>,IHandleMessages<Models.Commands.SummonElevator>
    {
        public GoToFloorHandler(ElevatorRequestRegister elevatorRequestRegister)
        {
            _elevatorRequestRegister = elevatorRequestRegister;
            
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

        public Task Handle(Models.Commands.SummonElevator message, IMessageHandlerContext context)
        {
            log.Info($"Received SummonElevator from {message.CurrentFloor}");
            _elevatorRequestRegister.SetFloor(message.CurrentFloor, message.RequestedDirection);

            var elevatorDispatched = new ElevatorDispatched { Floor = message.CurrentFloor, Direction = message.RequestedDirection };
            return context.Publish(elevatorDispatched);
        }
    }
}
