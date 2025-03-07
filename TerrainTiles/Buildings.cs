using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.TerrainTiles
{
    internal class Buildings : TerrainTile
    {
        public Buildings()
        {
            this.symbol = "OO";
            this.terrainInfo = "These are buildings.";
            this.travelWeight = 0.75;
        }
    }
}
