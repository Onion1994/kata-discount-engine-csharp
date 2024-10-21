using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    internal class UndiscountedItemsHandler
    {
        public void HandleUndiscountedItems(int daysUntilDate, Item item)
        {
            Money money = new Money(item.Price);
            if (daysUntilDate >= 6 && daysUntilDate <= 10)
            {
                money.LowerByPercent(0.05);
            }
            else if (daysUntilDate >= 0 && daysUntilDate <= 5)
            {
                money.LowerByPercent(0.10);
            }
            else if (daysUntilDate < 0)
            {
                money.LowerByPercent(0.20);
            }
            item.Price = money.Amount;
        }
    }
}
