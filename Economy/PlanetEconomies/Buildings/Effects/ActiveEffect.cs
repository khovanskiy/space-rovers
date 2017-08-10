using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Effects
{
    public class ActiveEffect 
    {        
        public int cooldown, cur_cooldown;
        public List<ElementaryEffect> el_effects;

        /// <summary>
        /// Описание активного эффекта
        /// </summary>
        /// <param name="cost">Стоимость активации</param>
        /// <param name="cooldown">Минимальное время между активациями</param>
        /// <param name="cur_cooldown">Текущее время до следующей активации</param>
        /// <param name="el_effects">Список элементарных эффектов, входящих в этот активный эффект</param>

        public ActiveEffect(int cooldown, int cur_cooldown, List<ElementaryEffect> el_effects)
        {
            this.cooldown = cooldown;
            this.cur_cooldown = cur_cooldown;
            this.el_effects = el_effects;
        }

        public ActiveEffect()
        {
        }
    }
    
}
