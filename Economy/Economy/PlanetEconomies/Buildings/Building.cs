using System.Collections.Generic;
using Engine.Economy.PlanetEconomies.Buildings.Actions;

namespace Engine.Economy.PlanetEconomies.Buildings
{    
    public class Building
    {
        public int building_id;                         
        public string building_name;                           
        public ConstructionConditions construction_props;            
        public List<int> continuous_actions;
        public List<int> repetitive_actions;
        public OperatingConditions operating_conditions;

        /// <summary>
        /// Описание постройки
        /// </summary>
        /// <param name="building_id">Идентификатор постройки</param>
        /// <param name="building_name">Название постройки</param>
        /// <param name="construction_props">Условия строительства</param>
        /// <param name="continuous_actions">Список продолжительных действий<param>        
        /// <param name="repetitive_actions">Список повторяющихся действий</param>
        /// <param name="operating_conditions">Связь работы постройки с эффективностью труда</param>
        
        public Building(int building_id, string building_name, 
            ConstructionConditions construction_props, 
            List<int> continuous_actions,            
            List<int> repetitive_actions,
            OperatingConditions operating_conditions)
        {
            this.building_id = building_id;
            this.building_name = building_name;            
            this.construction_props = construction_props;
            this.continuous_actions = continuous_actions;            
            this.repetitive_actions = repetitive_actions;
            this.operating_conditions = operating_conditions;
        }

        public Building()
        {
        }
    }
}
