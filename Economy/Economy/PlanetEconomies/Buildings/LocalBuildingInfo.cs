using System.Collections.Generic;
using Engine.Economy.PlanetEconomies.Buildings.Actions;

namespace Engine.Economy.PlanetEconomies.Buildings
{
    public class LocalBuildingsInfo
    {
        public int amount;
        public List<LocalRepAction> current_actions;
        public float current_efficiency;

        /// <summary>
        /// Информация о постройках данного типа, находящихся на планете
        /// </summary>
        /// <param name="amount">Количество построек</param>
        /// <param name="current_actions">Их повторяющиеся действия</param>
        
        public LocalBuildingsInfo(int amount, List<LocalRepAction> current_actions)
        {
            this.amount = amount;
            this.current_actions = current_actions;
        }

        public LocalBuildingsInfo()
        {
        }
    }    
}
