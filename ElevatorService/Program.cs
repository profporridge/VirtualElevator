using Models;
using NServiceBus;
using NServiceBus.Logging;
using System;
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
            routing.RouteToEndpoint(typeof(GoToFloor), "ElevatorController");
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            await RunLoop(endpointInstance)
               .ConfigureAwait(false);
            await endpointInstance.Stop()
                .ConfigureAwait(false);

        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to go to floor, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new GoToFloor
                        {
                            Floor = new Floor() { Id = 1}
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending GoToFloor command, Floor = {command.Floor}");
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
