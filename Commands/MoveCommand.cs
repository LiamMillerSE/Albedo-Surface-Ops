using Albedo_Surface_Ops.Units;
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
        bool completed = false;
        Unit unit;
        public MoveCommand(Unit u, Direction dir)
        {
            direction = dir;
            unit = u;
        }
        public void Execute()
        {
            completed = GameMaster.Instance().MoveUnit(this.unit, direction);
        }

        public bool IsComplete()
        {
            return completed;
        }
        public override string ToString()
        {
            return "Move " + direction.ToString() + " command";
        }
    }
}
