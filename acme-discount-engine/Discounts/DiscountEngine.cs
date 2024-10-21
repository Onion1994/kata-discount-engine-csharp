using AcmeSharedModels;
using System.Security.AccessControl;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private List<string> TwoForOneList = new List<string> { "Freddo" };
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };
        private TwoForOneDiscount TwoForOneDiscount = new TwoForOneDiscount();
        private PerishableItemHandler perishableItemHandler = new PerishableItemHandler();
        private UndiscountedItemsHandler undiscountedItemHandler = new UndiscountedItemsHandler();
        private BulkDiscountHandler bulkDiscountHandler = new BulkDiscountHandler();

        public double ApplyDiscounts(List<Item> items)
        {
            items.Sort((firstItem, secondItem) => firstItem.Name.CompareTo(secondItem.Name));

            TwoForOneDiscount.ApplyDiscount(items, TwoForOneList);


            double itemTotal = 0.00;
            foreach (var item in items)
            {
                itemTotal += item.Price;
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if(DateTime.Today > item.Date) { daysUntilDate = -1; }

                if (item.IsPerishable)
                {
                    perishableItemHandler.HandlePerishableItems(daysUntilDate, Time, item);
                }
                else if (!NoDiscount.Contains(item.Name))
                    {
                        undiscountedItemHandler.HandleUndiscountedItems(daysUntilDate, item);
                    }
                }

            bulkDiscountHandler.HandleBulkDiscount(items, TwoForOneList);

            double finalTotal = items.Sum(item => item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal -= finalTotal * 0.02;
            }

            return Math.Round(finalTotal, 2);
        }
    }
}

