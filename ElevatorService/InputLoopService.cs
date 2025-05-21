using Microsoft.Extensions.Hosting;
using Models;
using Models.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HallwayUI
{
    public class InputLoopService(IMessageSession messageSession, BuildingService buildingService) : BackgroundService
    {

        BuildingService _buildingService = buildingService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int _floor = 1;
            while (true)
            {
                Console.WriteLine($"Current Floor: {_buildingService.Floors[_floor].Name}");
                Console.WriteLine($"Panel: {_buildingService.HallwayInstances[_floor].Panel.StatusText()}");
                Console.WriteLine("Press 'U/D' to call elevator, 'UpArrow' to take the stairs up or 'DownArrow' to take the stairs down, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.U:
                    case ConsoleKey.D:
                        // Instantiate the command
                        var command = new RequestElevator()
                        {
                            CurrentFloor = new Models.Floor() { Id = _floor, Name = "Lobby" },
                            RequestedDirection = (key.Key == ConsoleKey.U) ? Models.Direction.Up : Models.Direction.Down
                        };

                        // Send the command
                        Console.WriteLine($"RequestElevator sent, floor = {command.CurrentFloor.Id}");
                        await messageSession.Send(command, stoppingToken);

                        break;
                    case ConsoleKey.UpArrow:
                        if (_floor < _buildingService.Floors.Count - 1)
                            _floor++;
                        else
                            Console.WriteLine("You are already on the top floor.");
             
                        break;
                    case ConsoleKey.DownArrow:
                        if (_floor > 0)
                            _floor--;
                        else
                            Console.WriteLine("You are already on the ground floor.");
                        break;
                    case ConsoleKey.Q:
                        return;

                    default:
                        Console.WriteLine("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
