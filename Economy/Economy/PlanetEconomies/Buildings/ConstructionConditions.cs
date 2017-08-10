namespace Engine.Economy.PlanetEconomies.Buildings
{
    public class ConstructionConditions
    {
        public float modules_cost; 
        public int cur_level, next_level_id, prev_level_id; 
        public float tech_level;      
        public bool[] fractions_allowed;       
        public bool unique_building, unique_branch;
             
        /// <summary>
        /// Условия строительства
        /// </summary>
        /// <param name="modules_cost">Стоимость строительства в модулях</param>
        /// <param name="tech_level">Необходимый уровень технологий</param>
        /// <param name="fractions_allowed">Фракции, которым доступна эта постройка</param>
        /// <param name="empl_workers">Персонал (рабочие)</param>
        /// <param name="empl_soldiers">Персонал (солдаты)</param>
        /// <param name="empl_engineers">Персонал (индженеры)</param>
        /// <param name="empl_scientists">Персонал (учёные)</param>
        /// <param name="unique_building">Нельзя строить больше 1 такого здания</param>
        /// <param name="unique_branch">Нельзя строить больше 1 такого здания любого уровня</param>
        /// <param name="cur_level">Уровень этого здания</param>
        /// <param name="prev_level_id">Идентификатор здания предыдущего уровня</param>
        /// <param name="next_level_id">Идентификатор здания следующего уровня</param>
        /// 
        public ConstructionConditions(float modules_cost, float tech_level, 
            bool[] fractions_allowed, int empl_workers, int empl_soldiers, 
            int empl_engineers, int empl_scientists, bool unique_building, 
            bool unique_branch, int cur_level, int prev_level_id, 
            int next_level_id)
        {
            this.modules_cost = modules_cost;
            this.tech_level = tech_level;
            this.fractions_allowed = fractions_allowed;            
            this.unique_branch = unique_branch;
            this.unique_building = unique_building;
            this.cur_level = cur_level;
            this.next_level_id = next_level_id;
            this.prev_level_id = prev_level_id;
        }

        public ConstructionConditions()
        {
        }
    }
}
