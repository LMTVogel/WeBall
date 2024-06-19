using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LogisticsShippingRateIsADecimal()
    {
        // Arrange
        var logisticsCompany = new LogisticsCompany("Test Company");

        // Act
        // Assert
        Assert.That(logisticsCompany.ShippingRate, Is.InstanceOf<decimal>());
    }

    [Test]
    public void LogisticsShippingRateIsBetween1And10()
    {
        for (var i = 0; i < 1_000; i++)
        {
            // Arrange
            var logisticsCompany = new LogisticsCompany("Test Company");
            // Act
            // Assert
            Assert.That(logisticsCompany.ShippingRate, Is.InRange(1, 10));
        }
    }
}