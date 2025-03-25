using Albedo_Surface_Ops.Commands;
using Albedo_Surface_Ops.Units;

namespace Albedo_Surface_Ops
{
    class FightController
    {
        List<Squad> squads;
        static Random rand = new Random();

        public FightController(List<Squad> invlolvedSquads)
        {
            squads = invlolvedSquads;
            foreach (Squad squad in squads)
            {
                //IGNORE ALL ORDERS AND FIGHT DAMN IT!
                squad.Engage();
            }
        }

        public void AddCombatant(Squad s)
        {
            squads.Add(s);
        }

        //returns true if the fight has ended
        public bool Update()
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
                else 
                {
                    //Fight is over
                    foreach(Squad s in squads)
                    {
                        s.EndFight();
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
