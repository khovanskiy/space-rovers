using System.Collections.Generic;
using Engine.RegistrySystem.Objects;
using Engine.Economy.PlanetEconomies.Buildings;
using Engine.Economy.PlanetEconomies.Buildings.Actions;
using Engine.Economy.PlanetEconomies.Shops;
using Engine.Economy.ERegistry;
using Engine.Economy.Core;

namespace Engine.Economy.PlanetEconomies
{   
    public class PlanetEconomy : RegistryObject
    {
        public int fraction_id;        
        public PlanetProperties basic_properties, current_properties;
        public LocalBuildingsInfo[] colony_buildings_info;        
        public PawnStore pawnstore;
        
        /// <summary>
        /// Экономика планеты
        /// </summary>
        /// <param name="my_id">Идентификатор экономики в реестре</param>
        /// <param name="inner_objects">Внутренние объекты в реестре</param>
        /// <param name="parent_object_id">Идентификатор родителя в реестре</param>
        /// <param name="fraction_id">Идентификатор фракции</param>
        /// <param name="basic_properties">Базовые свойства планеты</param>
        /// <param name="current_properties">Текущие свойства планеты</param>
        /// <param name="colony_buildings_info">Информация о постройках на планете</param>
        /// <param name="pawnstore">Магазин одиночных товаров планеты</param>

        public PlanetEconomy(string my_id, List<string> inner_objects,
            string parent_object_id, int fraction_id, 
            PlanetProperties basic_properties, PlanetProperties current_properties, 
            LocalBuildingsInfo[] colony_buildings_info, PawnStore pawnstore)
        {
            this.my_id = my_id;
            this.inner_objects = inner_objects;
            this.parent_object_id = parent_object_id;
            this.fraction_id = fraction_id;
            this.basic_properties = basic_properties;
            this.current_properties = current_properties; 
            this.colony_buildings_info = colony_buildings_info;           
            this.pawnstore = pawnstore;
        }

        public PlanetEconomy()
        {
        }

        public override void NextTick(float dt)
        {
            EconomyCore.NextEconomyTurn(this, dt);
        }
        
    }
}
