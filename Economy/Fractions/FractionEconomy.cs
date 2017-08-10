using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Economy.Fractions
{
    public class FractionEconomy
    {        
        public float technology, birth_rate, tax_rate;
        public string capital_planet_id;
        /// <summary>
        /// Экономика фракции
        /// </summary>
        /// <param name="capital_planet_id">Идентификатор столицы фракции</param>
        /// <param name="technology">Уровень технологий фракции</param>
        /// <param name="birth_rate">Рождаемость фракции</param>
        /// <param name="tax_rate">Налоговая ставка фракции</param>
        
        public FractionEconomy(string capital_planet_id, 
            float technology, float birth_rate,
            float tax_rate)
        {
            this.capital_planet_id = capital_planet_id;
            this.technology = technology;
            this.birth_rate = birth_rate;
            this.tax_rate = tax_rate;            
        }

        public FractionEconomy()
        {
        }
    }
}
