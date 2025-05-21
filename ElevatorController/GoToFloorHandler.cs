using Models;
using Models.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorController
{
    

    public class GoToFloorHandler : 
       // IHandleMessages<GoToFloor>,
        IHandleMessages<Models.Commands.RequestElevator>,
        IHandleMessages<Models.ButtonPressed>
    {
        public GoToFloorHandler(ElevatorRequestRegister elevatorRequestRegister)
        {
            _elevatorRequestRegister = elevatorRequestRegister;
            
        }
        static ILog log = LogManager.GetLogger<GoToFloorHandler>();
        private ElevatorRequestRegister _elevatorRequestRegister;
        private IElevatorMotionService _elevatorMotionService;

/*        public Task Handle(GoToFloor message, IMessageHandlerContext context)
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
        }*/

        public async Task Handle(ButtonPressed message, IMessageHandlerContext context)
        {
            log.Info($"Received {message.GetType()} from {message.SourceFloor}");
            if (_elevatorRequestRegister.SetFloor(new Floor() { Id = message.SourceFloor }, message.UpDirection))
            { await context.Publish(new ElevatorDispatched { Floor = new Floor() { Id = message.SourceFloor }, Direction = message.UpDirection }); }
            else
            {
                log.Info($"Elevator already dispatched to floor {message.SourceFloor}");
            }
        }

        public async Task Handle(RequestElevator message, IMessageHandlerContext context)
        {

            log.Info($"Received {message.GetType()} from floor {message.CurrentFloor.Name}"); 
            if (_elevatorRequestRegister.SetFloor(new Floor() { Id = message.CurrentFloor.Id }, message.RequestedDirection))
            { await context.Publish(new ElevatorDispatched { Floor = new Floor() { Id = message.CurrentFloor.Id }, Direction = message.RequestedDirection }); }
            else
            {
                log.Info($"Elevator already dispatched to floor {message.CurrentFloor.Id}");
            }
        }
    }
}
