using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.TerrainTiles
{
    internal class Rubble : TerrainTile
    {
        public Rubble()
        {
            this.symbol = "~*";
            this.terrainInfo = "Rubble. The remains of some structure, now nearly unrecognizable.";
            this.travelWeight = 0.25;
        }
    }
}
