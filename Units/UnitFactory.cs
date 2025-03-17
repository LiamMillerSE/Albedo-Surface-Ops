using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Units
{
    public class UnitFactory
    {
        public UnitFactory() 
        {
        }
        #region Singleton
        private static UnitFactory _instance;
        public static UnitFactory Instance()
        {
            if(_instance == null)
            {
                _instance = new UnitFactory();
            }
            return _instance;
        }
        #endregion
    }
    enum UnitType
    {
        Light_Infantry,
        NONE
    }
}
