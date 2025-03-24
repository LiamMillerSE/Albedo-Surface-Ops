namespace Albedo_Surface_Ops.Units.EDF
{
    abstract class EDFSquad : Squad
    {
        protected EDFSquad(int x, int y) : base(x, y)
        {
            this.faction = Faction.EDF;
        }
    }
}
