using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops
{
    internal class TerrainTile
    {
        string symbol;

        public TerrainTile(string symb)
        {
            symbol = symb;
        }

        internal void WriteToConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(symbol);
        }
    }
}
