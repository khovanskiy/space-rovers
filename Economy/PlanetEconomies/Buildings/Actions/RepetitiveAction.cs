using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Actions
{
    public class RepetitiveAction 
    {        
        public int cooldown;
        public List<ElementaryAction> el_actions;
        public float critical_efficiency;        
        
        /// <summary>
        /// Описание активного эффекта
        /// </summary>        
        /// <param name="cooldown">Время в ходах между активациями</param>
        /// <param name="cur_cooldown">Врямя в ходах до следующей активации</param>
        /// <param name="el_actions">Список элементарных действий</param>
        /// <param name="critical_efficiency">Критическая эффективность (ниже не действует)</param>

        public RepetitiveAction(int cooldown, List<ElementaryAction> el_actions,
            float critical_efficiency)
        {
            this.cooldown = cooldown;            
            this.el_actions = el_actions;
            this.critical_efficiency = critical_efficiency;
        }

        public RepetitiveAction()
        {
        }
    }
    
}
