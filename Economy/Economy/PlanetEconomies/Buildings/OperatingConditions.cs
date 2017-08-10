namespace Engine.Economy.PlanetEconomies.Buildings
{
    public class OperatingConditions
    {
        public float workers_contribution, engineers_contribution,
            scientists_contribution, soldiers_contribution, 
            automatic_contribution;       
        /// <summary>
        /// Условия работы постройки
        /// </summary>
        /// <param name="workers_contribution">Вклад рабочих в эффективность работы</param>
        /// <param name="engineers_contribution">Вклад инженеров в эффективность работы</param>
        /// <param name="scientists_contribution">Вклад учёных в эффективность работы</param>
        /// <param name="soldiers_contribution">Вклад солдат в эффективность работы</param>
        /// <param name="automatic_contribution">Эффективность без персонала</param>        

        public OperatingConditions(float workers_contribution, 
            float engineers_contribution, float scientists_contribution, 
            float soldiers_contribution, float automatic_contribution)
        {
            this.workers_contribution = workers_contribution;
            this.engineers_contribution = engineers_contribution;
            this.scientists_contribution = scientists_contribution;
            this.soldiers_contribution = soldiers_contribution;            
            this.automatic_contribution = automatic_contribution;
        }

        public OperatingConditions()
        {
        }

    }
}
