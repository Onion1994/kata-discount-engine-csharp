using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class Money
    {
        public double Amount { get; set; }
        public Money(double amount) {
            Amount = amount;
        }
        public void Add(double amount) {
            Amount += amount;
        }
    }
}
