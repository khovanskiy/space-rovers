namespace Engine.Economy.PlanetEconomies.Shops.Objects
{    
    public class Items
    {
        public int item_id;
        public int amount;

        public Items(int item_id = 0, int amount = 0)
        {
            this.item_id = item_id;
            this.amount = amount;
        }

        public Items()
        {
        }
    }
}
