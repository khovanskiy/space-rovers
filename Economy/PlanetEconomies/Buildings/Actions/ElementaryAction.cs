using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Actions
{  
    public class ElementaryAction
    {
        public enum PropType
        {
            Float = 0,
            Int = 1,
            Bool = 2,
            Item = 3,
        }

        public enum ActionType
        {
            Add = 0,
            Set = 1,
            Mul = 2,
            Create = 3,            
        }

        public int prop_type, prop_number, action_type;                    
        public float action_koef, low_eff_koef;
        public bool efficiency_connected;
        
        /// <summary>
        /// Элементарное действие на один из параметров планеты
        /// </summary>
        /// <param name="prop_type">Тип параметра</param>
        /// <param name="prop_number">Номер параметра</param>
        /// <param name="action_type">Тип действия</param>
        /// <param name="action_koef">Коэффициент действия</param>
        /// <param name="low_eff_koef">Коэффициент действия при низкой эффективности</param>
        /// <param name="efficiency_connected">Действие зависит от эффективности</param>
        
        public ElementaryAction(int prop_type, int prop_number, 
            int action_type, float action_koef, float low_eff_koef,
            bool efficiency_connected)
        {
            this.prop_type = prop_type;
            this.prop_number = prop_number;
            this.action_type = action_type;            
            this.action_koef = action_koef;
            this.low_eff_koef = low_eff_koef;
            this.efficiency_connected = efficiency_connected;
        }

        public ElementaryAction()
        {
        }
    }
}
