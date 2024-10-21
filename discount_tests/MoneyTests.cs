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
            double initialAmount = 100.00;

            // Act
            Money money = new Money(initialAmount);

            // Assert
            Assert.AreEqual(initialAmount, money.Amount);
        }
        [TestMethod]
        public void ShouldAddCorrectAmount()
        {
            // Arrange
            double initialAmount = 100.00;
            Money money = new Money(initialAmount);

            // Act
            money.Add(50.00);

            // Assert
            double expected = 150.00;
            Assert.AreEqual(expected, money.Amount);
        }
        [TestMethod]
        public void ShouldRaiseAmountByGivenPercentage()
        {
            // Arrange
            double initialAmount = 100.00;
            Money money = new Money(initialAmount);

            // Act
            money.RaiseByPercent(0.15);

            // Assert
            double expected = 115.00;
            Assert.AreEqual(expected, money.Amount);
        }
        [TestMethod]
        public void ShouldLowerAmountByGivenPercentage()
        {
            // Arrange
            double initialAmount = 100.00;
            Money money = new Money(initialAmount);

            // Act
            money.LowerByPercent(0.15);

            // Assert
            double expected = 85.00;
            Assert.AreEqual(expected, money.Amount);
        }
    }
}