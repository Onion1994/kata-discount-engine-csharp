using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    internal class TwoForOneDiscount
    {
        public void ApplyDiscount(List<Item> items, List<string> twoForOneList, string currentItem, int itemCount)
        {

            foreach (var item in items)
            {
                if (item.Name != currentItem)
                {
                    currentItem = item.Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 3 && twoForOneList.Contains(item.Name))
                    {
                        item.Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
        }
    }
}
