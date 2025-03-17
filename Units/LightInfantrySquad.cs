using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Units
{
    internal class LightInfantrySquad : Squad
    {
        public LightInfantrySquad(Faction faction, int x, int y) : base(x,y) 
        {
            this.faction = faction;
            this.name = "Light Infantry Squad";
            this.symbol = "LI";
        }
    }
}
