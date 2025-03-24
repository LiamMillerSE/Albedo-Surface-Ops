using Albedo_Surface_Ops.Units;

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
        Squad unit;
        public MoveCommand(Squad u, Direction dir)
        {
            direction = dir;
            unit = u;
        }
        public void Execute()
        {
            completed = GameMaster.Instance().MoveSquad(this.unit, direction);
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
