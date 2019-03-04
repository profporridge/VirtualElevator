using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BuildingInfo
    {
        public static List<Floor> Floors = new List<Floor> {
            new Floor {DisplayName = "Ground Floor", Id = 0 , Name = "G"},
            new Floor {DisplayName = "First Floor", Id = 1, Name = "1"},
            new Floor {DisplayName = "Second Floor", Id = 2, Name = "2"}
        };
    }
}
