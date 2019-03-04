using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class HallwayButtonPanel
    {
        public bool UpPressed = false, DownPressed = false;
        public string StatusText()
        {
            if (UpPressed && DownPressed) return "Both buttons are lit";
            if (UpPressed || DownPressed) return $"The {(UpPressed ? "Up" : "Down")} button is lit";
            return "No buttons are lit";
        }
    }
}
