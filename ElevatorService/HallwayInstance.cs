using Models;
using Models.Entities;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallwayUI
{
   public class HallwayInstance 
    {
        private HallwayButtonPanel panel;
        private Floor floor;

        public HallwayButtonPanel Panel => panel;

        public HallwayInstance(Floor floor)
        {
            this.panel = new HallwayButtonPanel();
            this.floor = floor;
        }
       
    }
}
