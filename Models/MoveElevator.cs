using NServiceBus;

namespace Models
{
    public class MoveElevator : ICommand
    {
        public Direction Direction { get; set; }
    }
}
