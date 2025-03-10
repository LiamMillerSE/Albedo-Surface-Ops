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
    }
    enum Faction
    {
        EDF,
        ILR,
        CIVILIAN
    }
}
