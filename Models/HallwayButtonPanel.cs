using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class HallwayButtonPanel
    {
        public bool UpLit = false, DownLit = false;
        public string StatusText()
        {
            if (UpLit && DownLit) return "Both buttons are lit";
            if (UpLit || DownLit) return $"The {(UpLit ? "Up" : "Down")} button is lit";
            return "No buttons are lit";
        }
        public override string ToString()
        {
            return StatusText();
        }
        public void Set(Direction direction)
        {
            if (direction == Direction.Up)
            {
                UpLit = true;
               
            }
            else if (direction == Direction.Down)
            {
                DownLit = true;
              
            }
        }
        public void Clear(Direction direction)
        {
            if (direction == Direction.Up)
            {
                UpLit = false;
            }
            else if (direction == Direction.Down)
            {
                DownLit = false;
            }
        }
    }
}
