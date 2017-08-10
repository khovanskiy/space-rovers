using System;
using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Fleet
{
    public class PlanetSpaceship
    {
        string spaceship_id, target_id;

        /// <summary>
        /// Корабль, принадлежащий планете
        /// </summary>
        /// <param name="spaceship_id">Идентификатор этого корабля в реестре</param>
        /// <param name="target_id">Идентификатор цели, заданной родной планетой</param>
        
        public PlanetSpaceship(string spaceship_id, string target_id)
        {
            this.spaceship_id = spaceship_id;
            this.target_id = target_id;
        }

        public PlanetSpaceship()
        {
        }
    }
}
