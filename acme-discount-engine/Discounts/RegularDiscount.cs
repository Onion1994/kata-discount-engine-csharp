//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AcmeSharedModels;

//namespace acme_discount_engine.Discounts
//{
//    public class RegularDiscount : IDiscountStrategy
//    {
//        PerishableItemHandler perishableItemHandler = new PerishableItemHandler();
//        NonPerishableItemHandler nonPerishableItemHandler = new NonPerishableItemHandler();
//        public void ApplyDiscount(List<Item> items)
//        {
//            Money itemTotal = new Money(0.00);

//            foreach (var item in items)
//            {
//                itemTotal.Add(item.Price);
//                int daysUntilDate = (item.Date - DateTime.Today).Days;
//                if (DateTime.Today > item.Date) { daysUntilDate = -1; }

//                if (item.IsPerishable)
//                {
//                    perishableItemHandler.HandlePerishableItems(daysUntilDate, Time, item);
//                }
//                else if (!NoDiscount.Contains(item.Name))
//                {
//                    nonPerishableItemHandler.HandleNonPerishableItems(daysUntilDate, item);
//                }
//            }
//        }
//    }
//}
