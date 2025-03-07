using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Units
{
    internal class LightInfantry : Unit
    {
        public LightInfantry(Faction faction) 
        {
            this.faction = faction;
            this.name = "Light Infantry";
            this.symbol = "LI";
        }
    }
}
