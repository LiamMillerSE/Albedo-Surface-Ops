using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Units
{
    public abstract class Unit
    {
        internal string name;
        internal Faction faction;
        internal string symbol;

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
