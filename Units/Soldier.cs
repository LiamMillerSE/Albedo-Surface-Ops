using System;
using System.Diagnostics.Metrics;

namespace Albedo_Surface_Ops.Units
{
    public class Soldier
    {
        Species _species;
        Service _service;
        Status _status;
        Faction _faction;
        int _deflection = 11;
        int _woundThreashold;
        string _pronoun;

        //Generate a random soldier
        public Soldier(Service service, Faction faction)
        {
            _service = service;
            _status = new Status();
            _faction = faction;

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
            return _pronoun + " an " + _faction.ToString() + " " + _service.ToString() + " soldier.\n\t" + 
                "This " + _species + " is " + _status.ToString() + ".";
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
        HEALTHY,
        WOUNDED,
        CRIPPLED,
        INCAPACITATED,
        DEVASTATED,
        DEAD
    }
}
