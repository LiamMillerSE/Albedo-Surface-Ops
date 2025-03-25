using System;
using System.Diagnostics.Metrics;

namespace Albedo_Surface_Ops.Units
{
    public class Soldier
    {
        static readonly Random random = new Random();
        Species _species;
        Service _service;
        Status _status;
        Faction _faction;
        string MOS;

        int _deflection = 11;
        int _woundThreashold;
        string _pronoun;
        int _moraleMax;
        int _moraleCurrent;
        const int MORALE_BUFF = 7; //this is to make up for Awe reducing drive after morale is done

        //Generate a random soldier
        public Soldier(Service service, Faction faction, string mos = "soldier")
        {
            _service = service;
            _status = new Status();
            _faction = faction;
            MOS = mos;

            if (faction == Faction.EDF)
            {
                Random rand = new Random();
                switch (rand.Next(3))
                {
                    case 0:
                        _pronoun = "He is";
                        break;
                    case 1:
                        _pronoun = "She is";
                        break;
                    case 2:
                        _pronoun = "They are";
                        break;
                }
                _species = (Species)(rand.Next(Enum.GetNames(typeof(Species)).Length));
            }
            else if (faction == Faction.ILR)
            {
                _pronoun = "He is";
                _species = Species.Rabbit;
            }
            _woundThreashold = GetThresholdFromSpecies(_species) + 5;
            _moraleMax = GetMoraleFromSpecies(_species) + MORALE_BUFF;
            _moraleCurrent = _moraleMax;
        }

        public void AttackTarget(Soldier target)
        {
            if (CanFight())
            {
                //check if attack die beats defence dice
                //TODO: make it make sense
                if (random.Next(10) > Math.Max(random.Next(8), random.Next(6)))
                {
                    int penCount = target.GetPenetrationCount();
                    if (penCount > 0)
                    {
                        MoraleBoost(1); //Yay, I landed a shot!
                        if(target.TakeDamage(10 + (10 * penCount)))
                        {
                            MoraleBoost(5); //URRAAA the bastard's dead
                        }
                    }
                }
            }
        }

        public void MoraleBoost(int moraleToAdd)
        {
            _moraleCurrent += moraleToAdd;
            if(_moraleCurrent > _moraleMax)
            {
                _moraleCurrent = _moraleMax;
            }
        }

        public void MoraleLoss(int moraleToRemove)
        {
            _moraleCurrent -= moraleToRemove;
            if(_moraleCurrent < 0)
            {
                _moraleCurrent = 0;
            }
        }

        //returns true if the unit is knocked out
        public bool TakeDamage(int damage)
        {
            //WHAT??? no injury calc??
            if (damage >= _woundThreashold + 40)
            {
                _status = (Status)Math.Max((int)Status.DEVASTATED, (int)_status);
            }
            else if (damage >= _woundThreashold + 20)
            {
                _status = (Status)Math.Max((int)Status.INCAPACITATED, (int)_status);
            }
            else if (damage >= _woundThreashold + 10)
            {
                MoraleLoss(3);
                _status = (Status)Math.Max((int)Status.CRIPPLED, (int)_status);
            }
            else if (damage >= _woundThreashold)
            {
                MoraleLoss(2);
                _status = (Status)Math.Max((int)Status.WOUNDED, (int)_status);
            }
            else
            {
                MoraleLoss(1);
            }
            return !CanFight();
        }

        public int GetPenetrationCount()
        {
            int diceCount = 0;
            int hits = 0;
            switch (_status)
            {
                case Status.CRIPPLED:
                    diceCount = 3;
                    break;
                case Status.WOUNDED:
                    diceCount = 2;
                    break;
                case Status.HEALTHY:
                    diceCount = 1;
                    break;
                default:
                    //STOP IT THEY'RE ALREADY DEAAADDD...
                    break;
            }
            for (int i = 0; i < diceCount; i++)
            {
                if (random.Next(20) + 1 >= _deflection)
                {
                    hits++;
                }
            }
            _moraleCurrent--;
            //MAYBE check for panic???
            return hits;
        }

        int GetMoraleFromSpecies(Species s)
        {
            switch (s)
            {
                case Species.Bird: return 8;
                case Species.Canine: return 9;
                case Species.Small_Cat: return 8;
                case Species.Great_Cat: return 8;
                case Species.Rabbit: return 8;
                case Species.Marsupial: return 8;
                case Species.Macropod: return 8;
                case Species.Echidna: return 9;
                case Species.Otter: return 8;
                case Species.Platypus: return 8;
                case Species.Skunk: return 8;
                case Species.Weasel: return 8;
                case Species.Mouse: return 8;
                case Species.Raccoon: return 8;
                case Species.Rat: return 8;
                case Species.Squirrel: return 8;
                case Species.Ungulate: return 8;
                case Species.Bear: return 8;
                case Species.Fox: return 8;
                default: return 0;
            }
        }
        int GetThresholdFromSpecies(Species s)
        {
            switch (s)
            {
                case Species.Bird:      return 10;
                case Species.Canine:    return 16;
                case Species.Small_Cat: return 16;
                case Species.Great_Cat: return 20;
                case Species.Rabbit:    return 14;
                case Species.Marsupial: return 12;
                case Species.Macropod:  return 16;
                case Species.Echidna:   return 12;
                case Species.Otter:     return 14;
                case Species.Platypus:  return 12;
                case Species.Skunk:     return 14;
                case Species.Weasel:    return 14;
                case Species.Mouse:     return 10;
                case Species.Raccoon:   return 12;
                case Species.Rat:       return 14;
                case Species.Squirrel:  return 12;
                case Species.Ungulate:  return 22;
                case Species.Bear:      return 24;
                case Species.Fox:       return 14;
                default: return 0;
            }
        }
        public override string ToString()
        {
            return _pronoun + " an " + _faction.ToString() + " " + _service.ToString().Replace('_', ' ') + " " + MOS + ".\n\t" + 
                "This " + _species.ToString().Replace('_', ' ') + " is " + _status.ToString() + ".";
        }

        internal bool CanFight()
        {
            return !IsPanicked() && IsConcious();
        }
        internal bool IsPanicked()
        {
            return _moraleCurrent > 0;
        }
        internal bool IsConcious()
        {
            return _status < Status.INCAPACITATED;
        }
        internal Faction GetFaction()
        {
            return _faction;
        }
    }
    public enum Species
    {
        Bird,
        Canine,
        Small_Cat,
        Great_Cat,
        Rabbit,
        Marsupial,
        Macropod,
        Echidna,
        Otter,
        Platypus,
        Skunk,
        Weasel,
        Mouse,
        Raccoon,
        Rat,
        Squirrel,
        Ungulate,
        Bear,
        Fox
    }
    public enum Status
    {
        HEALTHY = 0,
        WOUNDED = 1,
        CRIPPLED = 2,
        INCAPACITATED = 3,
        DEVASTATED = 4,
        DEAD = 5
    }
}
