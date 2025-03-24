using Albedo_Surface_Ops.Units.EDF;
using Albedo_Surface_Ops.Units.ILR;

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
                    if (faction == Faction.EDF)
                    {
                        squad = new EDF_LightInfantrySquad(x, y);
                        squad.AddSoldiers(GenerateSoldiers(Faction.EDF, service, EDF_LI_STRINGS));
                    }
                    else
                    {
                        squad = new ILR_LightInfantrySquad(x, y);
                        squad.AddSoldiers(GenerateSoldiers(Faction.ILR, service, ILR_LI_STRINGS));
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown Service: " + service.ToString());
            }
            return squad;
        }

        private List<Soldier> GenerateSoldiers(Faction f, Service s, List<string> unitTitles)
        {
            List<Soldier> soldiers = new List<Soldier>();
            for (int i = 0; i < unitTitles.Count; i++)
            {
                soldiers.Add(new Soldier(s, f, unitTitles[i]));
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

        private readonly List<string> EDF_LI_STRINGS = new List<string> { "Commander", "Assistant Squad Leader", "Soldier", "Soldier", "Soldier", "Soldier", "Support Weapon Specialist", "Platoon Specialist" };
        private readonly List<string> ILR_LI_STRINGS = new List<string> { "Commander", "Assistant Squad Leader", "Soldier", "Soldier", "Soldier", "Soldier", "Support Weapon Specialist", "Genadier" };
    }
    public enum Service
    {
        Light_Infantry,
        NONE
    }
}
