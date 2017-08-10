namespace Engine.Economy.PlanetEconomies
{   
    public class PlanetProperties
    {
        public static int Floats_Num = 19, Ints_Num = 14;

        public enum Floats
        {            
            colony_resources = 0,
            colony_income = 1,
            building_modules = 2,
            productive_capacity = 3,
            taxes_collected = 4,    
            birthrate = 5,
            margin_on_spaceships = 6,
            margin_on_equipment = 7,
            margin_on_services = 18,
            defective_spaceship_chance = 8,
            masterpiece_spaceship_chance = 9,
            defective_equipment_chance = 10,
            masterpiece_equipment_chance = 11,
            population_spirit = 12,
            engineers_education = 13,
            scientists_education = 14,
            soldiers_education = 15,
            current_safety = 16,
            teleport_distance = 17,            
        }

        public enum Ints
        {
            max_population = 0,
            total_population = 1,
            workers = 2,            
            scientists = 3,
            soldiers = 4,
            engineers = 5,            
            empl_engineers = 6,
            empl_soldiers = 7,
            empl_scientists = 8,
            empl_workers = 9,            
            equipment_shop_level = 10,
            service_station_level = 11,
            spaceship_shop_level = 12,
            teleport_level = 13,
        }
        
        public float[] prop_floats;
        public int[] prop_ints;

        public PlanetProperties(float[] prop_floats, int[] prop_ints)
        {            
            this.prop_floats = prop_floats;
            this.prop_ints = prop_ints;           
        }

        public PlanetProperties()
        {
        }
    }
}
