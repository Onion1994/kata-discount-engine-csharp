using Microsoft.VisualStudio.TestTools.UnitTesting;
using acme_discount_engine.Discounts;
namespace discount_tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void ShouldStoreCorrectAmount()
        {
            // Arrange
            double initialAmount = 100.00;  // Use decimal for currency

            // Act
            Money money = new Money(initialAmount);

            // Assert
            Assert.AreEqual(initialAmount, money.Amount);
        }
        [TestMethod]
        public void ShouldAddCorrectAmount()
        {
            // Arrange
            double initialAmount = 100.00;  // Use decimal for currency
            Money money = new Money(initialAmount);

            // Act
            money.Add(50.00);

            // Assert
            double expected = 150.00;
            Assert.AreEqual(expected, money.Amount);
        }
    }
}