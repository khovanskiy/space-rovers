using System.Collections.Generic;
using Engine.Economy.PlanetEconomies.Shops.Objects;

namespace Engine.Economy.PlanetEconomies.Shops
{    
    public class PawnStore
    {
        public List<Items> items;
        
        public PawnStore(List<Items> items)
        {
            this.items = items;
        }

        public PawnStore()
        {
        }

        public int ItemsLeft(Items item)
        {
            int amount = 0;
            foreach (Items i in this.items)
            {
                if (i.item_id == item.item_id) amount = i.amount;
            }
            return amount;
        }

        public void Buy(Items items)
        {
            foreach (Items i in this.items)
            {
                if (i.item_id == items.item_id)
                {
                    if (i.amount > items.amount) i.amount -= items.amount;
                    else this.items.Remove(i);
                }
            }
        }

        public void Sell(Items items)
        {
            if (ItemsLeft(items) == 0) this.items.Add(items);
            else foreach (Items i in this.items)
                {
                    if (i.item_id == items.item_id) i.amount += items.amount;
                }

        }
    }
}
