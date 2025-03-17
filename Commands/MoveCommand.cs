﻿using System;
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
        public MoveCommand(Direction dir)
        {
            direction = dir;
        }
        public void Execute()
        {
            //completed = 
        }

        public bool IsComplete()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Move " + direction.ToString() + " command";
        }
    }
}
