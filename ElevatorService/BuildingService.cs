using Models;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HallwayUI
{

    public class BuildingServiceHandler : IHandleMessages<ElevatorDispatched>, IHandleMessages<ElevatorArrivedAtFloor>
    {
        private readonly BuildingService _buildingService;
        public BuildingServiceHandler(BuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        public Task Handle(ElevatorDispatched message, IMessageHandlerContext context)
        {
            _buildingService.Handle(message, context);
            return Task.CompletedTask;
        }
        public Task Handle(ElevatorArrivedAtFloor message, IMessageHandlerContext context)
        {
            _buildingService.Handle(message, context);
            return Task.CompletedTask;
        }
    }
    public class BuildingService
    {
        private List<Floor> _floors = new List<Floor> { new Floor { DisplayName = "B", Name = "Basement", Id = 0 },
        new Floor { DisplayName = "L", Name = "Lobby", Id= 1 }
        , new Floor {DisplayName = "1", Name = "First Floor", Id = 2  }
        , new Floor {DisplayName = "2", Name = "Second Floor", Id = 3  }
        , new Floor {DisplayName = "3", Name = "Third Floor", Id = 4  }
        , new Floor {DisplayName = "4", Name = "Fourth Floor", Id = 5  }
        , new Floor {Id = 6 }
        , new Floor {Id = 7 }
, new Floor {Id = 8 }
, new Floor {Id = 9 } };

        public List<Floor> Floors
        {
            get { return _floors; }
            set { _floors = value; }
        }
        private readonly List<HallwayInstance> _hallwayInstances = new List<HallwayInstance>();

        public  List<HallwayInstance> HallwayInstances
        {
            get { return _hallwayInstances; }
            
        }
        public BuildingService()
        {
            _hallwayInstances = Floors.Select(f => new HallwayInstance(f)).ToList();
        }

        public Task Handle(ElevatorDispatched message, IMessageHandlerContext context)
        {

            _hallwayInstances[message.Floor.Id].Panel.Set(message.Direction);
            return Task.CompletedTask;
        }
        public Task Handle(ElevatorArrivedAtFloor message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }



    }
}
