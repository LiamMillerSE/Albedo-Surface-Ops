using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Commands
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST,
        none
    }
    public class MoveCommand : IUnitCommand
    {
        Direction direction;
        public MoveCommand(Direction dir)
        {
            direction = dir;
        }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
