using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Actions
{
    public class ContinuousAction
    {
        public List<ElementaryAction> el_actions;
        public float critical_efficiency;        
        
        /// <summary>
        /// Продолжительное действие на параметры планеты
        /// </summary>
        /// <param name="el_actions">Список элементарных действий</param>
        /// <param name="critical_efficiency">Критическая эффективность (ниже не действует)</param>
        
        public ContinuousAction(List<ElementaryAction> el_actions,
            float critical_efficiency)
        {
            this.el_actions = el_actions;
            this.critical_efficiency = critical_efficiency;
        }

        public ContinuousAction()
        {            
        }
    }
}
