using System;
using CarRent.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRent.UnitTests
{
  [TestClass]
  public class DatePeriodTests
  {
    [TestMethod]
    public void DatePeriodConstruction_ShouldCutTime()
    {
      var from = new DateTimeOffset(2017, 9, 15, 14, 43, 32, 10, TimeSpan.Zero);
      var to = DateTimeOffset.UtcNow;

      var period = new DatePeriod(from, to);

      Assert.AreEqual(period.From.Hour, 0);
      Assert.AreEqual(period.From.Minute, 0);
      Assert.AreEqual(period.From.Second, 0);
      Assert.AreEqual(period.From.Millisecond, 0);
    }

    [TestMethod]
    public void DoesIntersectTwoCrossingPeriods_ShouldReturnTrue()
    {
      var firstPeriod = new DatePeriod(
        new DateTimeOffset(2017, 5, 20, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero));
      var secondPeriod = new DatePeriod(
        new DateTimeOffset(2017, 6, 11, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 8, 2, 0, 0, 0, TimeSpan.Zero));

      var doesIntersect = firstPeriod.DoesIntersectWith(secondPeriod);

      Assert.IsTrue(doesIntersect);
    }

    [TestMethod]
    public void DoesIntersectTwoNotCrossingPeriods_ShouldReturnFalse()
    {
      var firstPeriod = new DatePeriod(
        new DateTimeOffset(2017, 5, 20, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero));
      var secondPeriod = new DatePeriod(
        new DateTimeOffset(2017, 8, 11, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 10, 2, 0, 0, 0, TimeSpan.Zero));

      var doesIntersect = firstPeriod.DoesIntersectWith(secondPeriod);

      Assert.IsFalse(doesIntersect);
    }

    [TestMethod]
    public void LaterNotCrossingPeriod_ShouldBeLater()
    {
      var firstPeriod = new DatePeriod(
        new DateTimeOffset(2017, 5, 20, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero));
      var secondPeriod = new DatePeriod(
        new DateTimeOffset(2017, 8, 11, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 10, 2, 0, 0, 0, TimeSpan.Zero));

      var isLater = secondPeriod.IsLaterThan(firstPeriod);

      Assert.IsTrue(isLater);
    }

    [TestMethod]
    public void EarlierNotCrossingPeriod_ShouldNotBeLater()
    {
      var firstPeriod = new DatePeriod(
        new DateTimeOffset(2017, 5, 20, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero));
      var secondPeriod = new DatePeriod(
        new DateTimeOffset(2017, 8, 11, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 10, 2, 0, 0, 0, TimeSpan.Zero));

      var isLater = firstPeriod.IsLaterThan(secondPeriod);

      Assert.IsFalse(isLater);
    }

    [TestMethod]
    public void CrossingPeriods_ShouldNotBeLater()
    {
      var firstPeriod = new DatePeriod(
        new DateTimeOffset(2017, 5, 20, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 12, 1, 0, 0, 0, TimeSpan.Zero));
      var secondPeriod = new DatePeriod(
        new DateTimeOffset(2017, 8, 11, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 10, 2, 0, 0, 0, TimeSpan.Zero));

      var isLater = firstPeriod.IsLaterThan(secondPeriod);

      Assert.IsFalse(isLater);
    }
  }
}
