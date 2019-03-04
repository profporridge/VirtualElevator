using Models;
using Models.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title ="ElevatorButton";
            var endpointConfiguration = new EndpointConfiguration("ElevatorButton");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(SummonElevator), "ElevatorController");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            await RunLoop(endpointInstance).ConfigureAwait(false);
            await endpointInstance.Stop().ConfigureAwait(false);

        }

        static ILog log = LogManager.GetLogger<Program>();

        static Models.Entities.HallwayButtonPanel hallwayButtonPanel = new Models.Entities.HallwayButtonPanel();
        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            int currentFloor = 0;
            log.Info("Press 'U' to go to Up, 'D' to go Down, or 'Q' to quit.");
            while (true)
            {
                log.Info($"You are on floor {currentFloor}. {hallwayButtonPanel.StatusText()}");
                
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.U:
                    case ConsoleKey.D:
                        // Instantiate the command
                        var command = new SummonElevator
                        {
                            CurrentFloor = new Floor() { Id = currentFloor },
                            RequestedDirection = key.Key==ConsoleKey.U? Direction.Up : Direction.Down
                        };

                        // Send the command to the local endpoint
                        //log.Info($"Sending GoToFloor command, Floor = {command.Floor}");
                        await endpointInstance.Send(command)
                            .ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
