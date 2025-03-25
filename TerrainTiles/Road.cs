using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.TerrainTiles
{
    internal class Road : TerrainTile
    {
        public Road() 
        {
            this.symbol = "##";//TODO: make this 'smarter'
            this.terrainInfo = "A road, this will make travel nice and quick!";
            this.travelWeight = 0.001;
        }
    }
}
