﻿using AcmeSharedModels;
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

        public double ApplyDiscounts(List<Item> items)
        {
            items.Sort((firstItem, secondItem) => firstItem.Name.CompareTo(secondItem.Name));
            string currentItem = string.Empty;
            int itemCount = 0;

            TwoForOneDiscount.ApplyDiscount(items, TwoForOneList, currentItem, itemCount);

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
                else
                {
                    if (!NoDiscount.Contains(item.Name))
                    {
                        if (daysUntilDate >= 6 && daysUntilDate <= 10)
                        {
                            item.Price -= item.Price * 0.05;
                        }
                        else if (daysUntilDate >= 0 && daysUntilDate <= 5)
                        {
                            item.Price -= item.Price * 0.10;
                        }
                        else if (daysUntilDate < 0)
                        {
                            item.Price -= item.Price * 0.20;
                        }
                    }
                }
            }

            currentItem = string.Empty;
            itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name != currentItem)
                {
                    currentItem = items[i].Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 10 && !TwoForOneList.Contains(items[i].Name) && items[i].Price >= 5.00)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            items[i - j].Price -= items[i - j].Price * 0.02;
                        }
                        itemCount = 0;
                    }
                }
            }

            double finalTotal = items.Sum(item => item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal -= finalTotal * 0.02;
            }

            return Math.Round(finalTotal, 2);
        }
    }
}

