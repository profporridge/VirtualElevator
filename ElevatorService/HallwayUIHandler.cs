using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using NServiceBus;

namespace HallwayUI
{
    class HallwayUIHandler : IHandleMessages<Models.ElevatorDispatched>
    {
        public async Task Handle(ElevatorDispatched message, IMessageHandlerContext context)
        
        {
            Console.WriteLine($"Elevator dispatched to {message.Floor.Id}");
        }
    }
    }

