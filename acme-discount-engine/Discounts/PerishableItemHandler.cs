using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    internal class PerishableItemHandler
    {
        public void HandlePerishableItems(int daysUntilDate, DateTime Time, Item item)
        {
            if (daysUntilDate == 0)
            {
            Money money = new Money(item.Price);
                if (Time.Hour >= 0 && Time.Hour < 12)
                {
                    money.LowerByPercent(0.05);
                }
                else if (Time.Hour >= 12 && Time.Hour < 16)
                {
                    money.LowerByPercent(0.10);
                }
                else if (Time.Hour >= 16 && Time.Hour < 18)
                {
                    money.LowerByPercent(0.15);
                }
                else if (Time.Hour >= 18)
                {
                    if (item.Name.Contains("(Meat)"))
                    {
                        money.LowerByPercent(0.15);
                    }
                    else
                    {
                        money.LowerByPercent(0.25);
                    }
                }
                item.Price = money.Amount;
            }
        }
    }
}
