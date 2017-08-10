using System.Collections.Generic;
using System.Collections;
using Engine.Economy.PlanetEconomies.Buildings;
using Engine.Economy.PlanetEconomies.Buildings.Actions;
using Engine.Economy.PlanetEconomies.Shops;
using Engine.Economy.PlanetEconomies.Shops.Objects;
using Engine.Economy.Fractions;

namespace Engine.Economy.ERegistry
{    
    public class EconomyRegistry
    {
        public enum Relation
        {
            Enemy = 0,
            Neutral = 1,
            Alliance = 2,
        }

        private static EconomyRegistry Instance;
        public int Fractions_Num, Buildings_Num, Items_Num, Equipment_Shops_Num, 
            Spaceship_Shops_Num, Continuous_Actions_Num, Repetitive_Actions_Num;
        public int[][] Relationships;
        public FractionEconomy[] FractionEconomies;
        public List<string>[] FractionPlanetEconomyIds;
        public Hashtable[] DefenceRewards, AttackRewards;
        public Hashtable SystemOfPlanet;
        public RepetitiveAction[] RepetitiveActions;
        public ContinuousAction[] ContinuousActions;
        public Building[] Buildings;
        public Shop[] EquipmentShops, SpaceshipShops;
        public Item[] Items;
    
        public EconomyRegistry()
        {            
            this.Fractions_Num = 5;
            this.Buildings_Num = 20;
            this.Equipment_Shops_Num = 5;
            this.Spaceship_Shops_Num = 5;
            this.Items_Num = 200;
            this.Continuous_Actions_Num = 100;
            this.Repetitive_Actions_Num = 200;
            this.RepetitiveActions = new RepetitiveAction[this.Repetitive_Actions_Num];
            this.ContinuousActions = new ContinuousAction[this.Continuous_Actions_Num];
            this.FractionEconomies = new FractionEconomy[this.Fractions_Num];
            this.FractionPlanetEconomyIds = new List<string>[this.Fractions_Num];
            this.Relationships = new int[this.Fractions_Num][];
            this.SystemOfPlanet = new Hashtable();
            for (int i = 0; i < this.Fractions_Num; ++i)
            {
                this.Relationships[i] = new int[this.Fractions_Num];
                this.FractionPlanetEconomyIds[i] = new List<string>();
                this.DefenceRewards[i] = new Hashtable();
                this.AttackRewards[i] = new Hashtable();
            }
            this.Buildings = new Building[this.Buildings_Num];
            this.Items = new Item[this.Items_Num];
            this.SpaceshipShops = new Shop[this.Spaceship_Shops_Num];
            this.EquipmentShops = new Shop[Equipment_Shops_Num];
        }

        public static EconomyRegistry getInstance()
        {
            if (Instance == null) Instance = new EconomyRegistry();
            return Instance;
        }
    }
}
