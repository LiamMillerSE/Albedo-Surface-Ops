using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.TerrainTiles
{

    public abstract class TerrainTile
    {
        internal string symbol;
        internal string terrainInfo;
        internal double travelWeight; //This indicates how much of an impact the tile has on navigation, higher numbers slow units down more

        internal void WriteToConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(symbol);
        }

        internal void WriteWeightToConsole()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            if(travelWeight >= 0) 
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            if(travelWeight > 0.2)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            if (travelWeight > 0.4)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            if (travelWeight > 0.6)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            if (travelWeight >= 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            Console.Write(travelWeight.ToString("n2").Substring(2));
        }

        public override string ToString()
        {
            return terrainInfo;
        }
    }
}
