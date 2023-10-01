using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Stats;
using UnityEngine;

namespace PlayerLogic.Stats
{
    public class ClassStats : IStatsProvider
    {
        private readonly ClassType _classType;  

        public ClassStats(ClassType classType)
        {
            _classType = classType;
        }


        public PlayerStats GetStats()
        {
            switch (_classType)
            {
                case ClassType.Warrior:
                    return new PlayerStats(
                        new PlayerStat("Health", 10, 10),
                        new PlayerStat("Damage", 5),
                        new Stamina("Stamina", 10, 10, 0.1f, 2),
                        new Mana("Mana", 2, 2),
                        new PlayerStat("Intelligence", 1),
                        new PlayerStat("Protection", 4),
                        new PlayerStat("Dexterity", 1));

                /*case ClassType.Archers:
                return new PlayerStats()
                {
                    Health = new PlayerStat(12,12),
                    Damage = new PlayerStat(4),
                    Stamina = new PlayerStat(5, 5),
                    Mana = new PlayerStat(2,2),
                    Intelligence = new PlayerStat(4),
                    Protection = new PlayerStat(2),
                    Dexterity = new PlayerStat(5)
                };
            case ClassType.Magician:
                return new PlayerStats()
                {
                    Health = new PlayerStat(8, 8),
                    Damage = new PlayerStat(2),
                    Stamina = new PlayerStat(3,3),
                    Mana = new PlayerStat(7,7),
                    Intelligence = new PlayerStat(7),
                    Protection = new PlayerStat(1),
                    Dexterity = new PlayerStat(1)
                };
            case ClassType.Thief:
                return new PlayerStats()
                {
                    Health = new PlayerStat(9, 9),
                    Damage = new PlayerStat(5),
                    Stamina = new PlayerStat(5,5),
                    Mana = new PlayerStat(1,1),
                    Intelligence = new PlayerStat(2),
                    Protection = new PlayerStat(2),
                    Dexterity = new PlayerStat(8)
                };*/
                default:
                    throw new NotImplementedException($"ClassType {_classType}, not found");
            }
        }
    }

    [Serializable]
    public enum ClassType
    {
        Warrior,
        Magician,
        Archers,
        Thief
    }
}