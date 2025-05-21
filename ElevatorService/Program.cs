using Models;
using Models.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NServiceBus.Hosting.Helpers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HallwayUI

{
    class Program
    {

      
        static async Task Main(string[] args)
        {
            Console.Title = "HallwayUI";
            var builder = Host.CreateApplicationBuilder(args);

            var endpointConfiguration = new EndpointConfiguration("HallwayUI");
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        endpointConfiguration.RegisterComponents(registration: configureComponents =>
        {
            configureComponents.AddSingleton(new BuildingService());
        });
            var transport = endpointConfiguration.UseTransport(new LearningTransport());
            transport.RouteToEndpoint(typeof(RequestElevator), "ElevatorController");
            // Fix: Ensure the UseNServiceBus extension method is available by adding the correct package and using directive
            builder.UseNServiceBus( endpointConfiguration);
            // Add this line:
            builder.Services.AddHostedService<InputLoopService>();
            //builder.Services.AddHostedService<BuildingService>();

            var app = builder.Build();

            // Uncomment and use app.RunAsync() if needed
            await app.RunAsync();
        }
    }
}
