using Albedo_Surface_Ops.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Albedo_Surface_Ops.Units
{
    public abstract class Unit
    {
        internal string name;
        internal Faction faction;
        internal string symbol;
        internal Queue<IUnitCommand> commands = new Queue<IUnitCommand>();
        bool isInCombat = false;
        public int x;
        public int y;

        protected Unit(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        internal void WriteToConsole()
        {
            switch (faction)
            {
                case Faction.EDF:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Faction.ILR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Faction.CIVILIAN:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            Console.Write(symbol);
        }

        public void IssueCommand(IUnitCommand command, bool overrideAll = false)
        {
            if(overrideAll) 
            {
                commands.Clear();
            }
            commands.Enqueue(command);
        }

        public void Battle(Unit target)
        {
            isInCombat = true;
            target.isInCombat = true;
            //TODO: Implement
            //Possibly queue a fight command that does not finish until the target dies/disengages, or the unit flees
        }

        public void Update()
        {
            if (commands.Count > 0)
            {
                commands.Peek().Execute();
                if (commands.Peek().IsComplete())
                {
                    commands.Dequeue();
                }
            }
        }

        public override string ToString()
        {
            switch (faction)
            {
                case Faction.EDF:
                    return "Friendly " + name;
                case Faction.ILR:
                    return "Hostile " + name;
                case Faction.CIVILIAN:
                    return "Unaligned " + name;
                default:
                    throw new Exception("No faction set.");
            }
        }

        internal Faction GetFaction()
        {
            return faction;
        }

        public bool CanMove()
        {
            return isInCombat;
        }
    }
    enum Faction
    {
        EDF,
        ILR,
        CIVILIAN
    }
}
