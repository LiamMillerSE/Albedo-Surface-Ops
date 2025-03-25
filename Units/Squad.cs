using Albedo_Surface_Ops.Commands;

namespace Albedo_Surface_Ops.Units
{
    public abstract class Squad
    {
        internal string name;
        internal Faction faction;
        internal string symbol;
        internal Queue<IUnitCommand> commands = new Queue<IUnitCommand>();
        bool isInCombat = false;
        public int x;
        public int y;
        List<Soldier> _soldiers = new List<Soldier>();

        protected Squad(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void AddSoldier(Soldier s)
        {
            _soldiers.Add(s);
        }
        public void AddSoldiers(List<Soldier> s)
        {
            _soldiers.AddRange(s);
        }

        internal void WriteToConsole()
        {
            switch (faction)
            {
                case Faction.EDF:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Faction.ILR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Faction.CIVILIAN:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            Console.Write(symbol);
        }

        public void IssueCommand(IUnitCommand command, bool overrideAll = false)
        {
            if(overrideAll) 
            {
                commands.Clear();
            }
            commands.Enqueue(command);
        }

        public void Update()
        {
            if (commands.Count > 0)
            {
                commands.Peek().Execute();
                if (commands.Peek().IsComplete())
                {
                    commands.Dequeue();
                }
            }
        }

        //Set Squad as fighting
        public void Engage()
        {
            this.isInCombat = true;
        }

        public bool CanFight()
        {
            return _soldiers.Any(s => s.CanFight());
        }
        public List<Soldier> GetSoldiers() { return _soldiers; }

        public override string ToString()
        {
            switch (faction)
            {
                case Faction.EDF:
                    return "Friendly " + name;
                case Faction.ILR:
                    return "Hostile " + name;
                case Faction.CIVILIAN:
                    return "Unaligned " + name;
                default:
                    throw new Exception("No faction set.");
            }
        }

        public string GetSquadInfo()
        {
            string info = this.ToString() + ":\n";
            foreach (Soldier s in _soldiers)
            {
                info += s.ToString() + "\n";
            }
            return info;
        }

        internal Faction GetFaction()
        {
            return faction;
        }

        public bool CanMove()
        {
            return !isInCombat;
        }

        internal void EndFight()
        {
            //Are we in fighting shape?
            if(_soldiers.FirstOrDefault(u=>u.CanFight()) != null)
            {
                isInCombat = false;
            }
            else
            {
                GameMaster.Instance().RemoveSquad(this);
            }
        }
    }
    public enum Faction
    {
        EDF,
        ILR,
        CIVILIAN
    }
}
