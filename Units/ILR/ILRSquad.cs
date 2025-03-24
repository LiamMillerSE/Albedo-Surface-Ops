namespace Albedo_Surface_Ops.Units.ILR
{
    abstract class ILRSquad : Squad
    {
        protected ILRSquad(int x, int y) : base(x, y)
        {
            this.faction = Faction.ILR;
        }
    }
}
