namespace Albedo_Surface_Ops.Units
{
    public class UnitFactory
    {
        public UnitFactory() 
        {
        }

        public Squad CreateSquad(Faction faction, Service service, int x, int y)
        {
            Squad squad = null;
            switch(service)
            {
                case Service.Light_Infantry:
                    squad = new LightInfantrySquad(faction, x, y);
                    squad.AddSoldiers(GenerateNSoldiers(8, faction, service));
                    break;
                default:
                    throw new ArgumentException("Unknown Service: " + service.ToString());
            }
            return squad;
        }

        private List<Soldier> GenerateNSoldiers(int n, Faction f, Service s)
        {
            List<Soldier> soldiers = new List<Soldier>();
            for (int i = 0; i < n; i++)
            {
                soldiers.Add(new Soldier(s, f));
            }
            return soldiers;
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
    public enum Service
    {
        Light_Infantry,
        NONE
    }
}
