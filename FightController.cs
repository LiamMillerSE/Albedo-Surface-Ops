using Albedo_Surface_Ops.Commands;
using Albedo_Surface_Ops.Units;

namespace Albedo_Surface_Ops
{
    class FightController
    {
        public FightCommand sharedCommand;
        List<Squad> squads;
        static Random rand = new Random();

        public FightController(List<Squad> invlolvedSquads)
        {
            //TODO: flesh the command out
            sharedCommand = new FightCommand();
            squads = invlolvedSquads;
            foreach (Squad squad in squads)
            {
                //IGNORE ALL ORDERS AND FIGHT DAMN IT!
                squad.IssueCommand(sharedCommand, true);
            }
        }

        public void AddCombatant(Squad s)
        {
            s.IssueCommand(sharedCommand, true);
            squads.Add(s);
        }

        public void Update()
        {
            //Get all the soldiers
            List<Soldier> soldiers = new List<Soldier>();
            List<Soldier> aliveILRguys = new List<Soldier>();
            List<Soldier> aliveEDFguys = new List<Soldier>();
            foreach (Squad s in squads)
            {
                if(s.GetFaction() == Faction.EDF)
                {
                    aliveEDFguys.AddRange(s.GetSoldiers().Where(solj => solj.CanFight()));
                }
                else if(s.GetFaction() == Faction.ILR)
                {
                    aliveILRguys.AddRange(s.GetSoldiers().Where(solj => solj.CanFight()));
                }
                soldiers.AddRange(s.GetSoldiers());
            }
            //Scramble 'em
            Queue<Soldier> attackOrder = new Queue<Soldier>();
            while (soldiers.Count > 0)
            {
                int index = rand.Next(soldiers.Count);
                attackOrder.Enqueue(soldiers[index]);
                soldiers.RemoveAt(index);
            }
            while(attackOrder.Count > 0)
            {
                Soldier attacker = attackOrder.Dequeue();
                Soldier defender = null;
                if(attacker.GetFaction() == Faction.EDF && aliveILRguys.Count > 0)
                {
                    defender = aliveILRguys[rand.Next(aliveILRguys.Count)];
                }
                else if (attacker.GetFaction() == Faction.ILR && aliveEDFguys.Count > 0)
                {
                    defender = aliveEDFguys[rand.Next(aliveEDFguys.Count)];
                }
                if (defender != null)
                {
                    //KILL THEM!!!
                    attacker.AttackTarget(defender);
                }
            }
        }
    }
}
