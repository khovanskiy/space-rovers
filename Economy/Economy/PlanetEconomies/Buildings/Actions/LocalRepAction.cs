using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Economy.PlanetEconomies.Buildings.Actions
{
    public class LocalRepAction
    {
        public int rep_action_id;
        public int cur_cooldown;

        /// <summary>
        /// Повторяющееся действие на конретной планете
        /// </summary>
        /// <param name="rep_action_id">Идентификатор повторяющегося действия</param>
        /// <param name="cur_cooldown">Текущее время в ходах до следующей активации</param>

        public LocalRepAction(int rep_action_id, int cur_cooldown)
        {
            this.rep_action_id = rep_action_id;
            this.cur_cooldown = cur_cooldown;
        }

        public LocalRepAction()
        {
        }
    }
}
