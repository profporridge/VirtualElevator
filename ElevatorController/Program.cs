using Microsoft.Extensions.Hosting;
using NServiceBus;
using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace ElevatorController
{
    class Program
    {
        private static readonly int NUMBER_OF_FLOORS = 10;

        static async Task Main(string[] args)
        {
            Console.Title = "ElevatorController";


            var builder = Host.CreateApplicationBuilder(args);

            var endpointConfiguration = new EndpointConfiguration("ElevatorController");
            endpointConfiguration.RegisterComponents(
    registration: configureComponents =>
    {
        configureComponents.AddSingleton(new ElevatorRequestRegister(NUMBER_OF_FLOORS));
    });
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            endpointConfiguration.UseTransport(new LearningTransport());

            builder.UseNServiceBus(endpointConfiguration);

            var app = builder.Build();

            await app.RunAsync();



            //        var endpointConfiguration = new EndpointConfiguration("ElevatorController");

            
            //        var transport = endpointConfiguration.UseTransport<LearningTransport>();
            //        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            //        Console.ReadLine();
            //        await endpointInstance.Stop()
            //            .ConfigureAwait(false);

            //    }
        }
    }
}
