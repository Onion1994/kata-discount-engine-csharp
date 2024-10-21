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
        private TwoForOneDiscount TwoForOneDiscount;
        private PerishableItemHandler perishableItemHandler = new PerishableItemHandler();
        private NonPerishableItemHandler undiscountedItemHandler = new NonPerishableItemHandler();
        private BulkDiscountHandler bulkDiscountHandler = new BulkDiscountHandler();

        public DiscountEngine()
        {
            TwoForOneDiscount = new TwoForOneDiscount(TwoForOneList);
        }

        public double ApplyDiscounts(List<Item> items)
        {
            TwoForOneDiscount = new TwoForOneDiscount(TwoForOneList);
            items.Sort((firstItem, secondItem) => firstItem.Name.CompareTo(secondItem.Name));

            TwoForOneDiscount.ApplyDiscount(items);
            Money itemTotal = new Money(0.00);

            foreach (var item in items)
            {
                itemTotal.Add(item.Price);
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if(DateTime.Today > item.Date) { daysUntilDate = -1; }

                if (item.IsPerishable)
                {
                    perishableItemHandler.HandlePerishableItems(daysUntilDate, Time, item);
                }
                else if (!NoDiscount.Contains(item.Name))
                    {
                        undiscountedItemHandler.HandleNonPerishableItems(daysUntilDate, item);
                    }
                }

            bulkDiscountHandler.HandleBulkDiscount(items, TwoForOneList);

            Money finalTotal = new Money(items.Sum(item => item.Price));

            if (LoyaltyCard && itemTotal.Amount >= 50.00)
            {
                finalTotal.LowerByPercent(0.02);
            }

            return Math.Round(finalTotal.Amount, 2);
        }
    }
}

