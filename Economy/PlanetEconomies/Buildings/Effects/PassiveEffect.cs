using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Effects
{
    public class PassiveEffect
    {
        public List<ElementaryEffect> el_effects;

        /// <summary>
        /// Описание пассивного эффекта
        /// </summary>
        /// <param name="el_effects">Список элементарных эффектов, входящих в этот пассивный эффект</param>

        public PassiveEffect(List<ElementaryEffect> el_effects)
        {
            this.el_effects = el_effects;
        }

        public PassiveEffect()
        {            
        }
    }
}
