﻿using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Models.Commands
{
    class GetConfiguration   
    {

    }
    public class RequestElevator: ICommand
    {
        public Floor CurrentFloor { get; set; }
        public Direction RequestedDirection { get; set; }
    }
}
