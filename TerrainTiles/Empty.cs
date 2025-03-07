using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.TerrainTiles
{
    internal class Empty : TerrainTile
    {
        public Empty()
        {
            this.symbol = "..";
            this.terrainInfo = "This is open air. Units (and bullets) will move freely through it";
            this.travelWeight = 0;
        }
    }
}
