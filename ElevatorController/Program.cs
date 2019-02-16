using NServiceBus;
using System;
using System.Threading.Tasks;

namespace ElevatorController
{
    class Program
    {
        private static readonly int NUMBER_OF_FLOORS = 10;

        static async Task Main(string[] args)
        {
            Console.Title = "ElevatorController";
            var endpointConfiguration = new EndpointConfiguration("ElevatorController");

            endpointConfiguration.RegisterComponents(
    registration: configureComponents =>
    {
        configureComponents.RegisterSingleton(new ElevatorRequestRegister(NUMBER_OF_FLOORS));
    });
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
           
            Console.ReadLine();
            await endpointInstance.Stop()
                .ConfigureAwait(false);

        }
    }
}
