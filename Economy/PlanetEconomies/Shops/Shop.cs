using System.Collections.Generic;

namespace Engine.Economy.PlanetEconomies.Shops
{
    public class Shop
    {
        public List<int> item_ids;
        public Shop(List<int> item_ids)
        {
            this.item_ids = item_ids;
        }

        public Shop()
        {
        }

    }
}
