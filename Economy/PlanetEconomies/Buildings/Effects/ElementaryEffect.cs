using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Buildings.Effects
{  
    public class ElementaryEffect
    {
        public enum PropType
        {
            Float = 0,
            Int = 1,
            Bool = 2,
            Item = 3,
        }

        public enum EffectType
        {
            Add = 0,
            Set = 1,
            Mul = 2,
            Create = 3,
        }

        public int prop_type, prop_number, effect_type;                    
        public float effect_koef;          

        /// <summary>
        /// Описание элементарного эффекта
        /// </summary>
        /// <param name="prop_type">Тип характеристики, на которую оказывается влияние</param>
        /// <param name="prop_number">Номер соответствующей ххарактеристики</param>
        /// <param name="effect">Способ воздействия на эту характеристику</param>
        /// <param name="eff_koef">Коэффициент воздействия</param>

        public ElementaryEffect(int prop_type = 0, int prop_number = 0, int effect_type = 0, float effect_koef = 0)
        {
            this.prop_type = prop_type;
            this.prop_number = prop_number;
            this.effect_type = effect_type;            
            this.effect_koef = effect_koef;
        }

        public ElementaryEffect()
        {
        }
    }
}
