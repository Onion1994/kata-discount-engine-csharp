using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    internal interface IDiscountStrategy
    {
        public void ApplyDiscount(List<Item> items);
    }
}
