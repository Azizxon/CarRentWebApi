using System;
using CarRent.Common;
using CarRent.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRent.UnitTests
{
  [TestClass]
  public class CarScheduleTests
  {
    [TestMethod]
    public void IsFreeOnPeriodCrossingTwoPeriods_ShouldBeFalse()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented),
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2016, 6, 2, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero)), 
          OccupationStatus.Rented)
      });

      var isFree = carSchedule.IsFreeOnPeriod(new DatePeriod(
        new DateTimeOffset(2016, 5, 1, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2016, 7, 1, 0, 0, 0, TimeSpan.Zero)));

      Assert.IsFalse(isFree);
    }

    [TestMethod]
    public void IsFreeOnPeriodCrossingOnePeriod_ShouldBeFalse()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(new DatePeriod(
            new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented)
      });

      var isFree = carSchedule.IsFreeOnPeriod(
        new DatePeriod(
          new DateTimeOffset(2015, 11, 15, 0, 0, 0, TimeSpan.Zero),
          new DateTimeOffset(2016, 2, 10, 0, 0, 0, TimeSpan.Zero)));

      Assert.IsFalse(isFree);
    }

    [TestMethod]
    public void IsFreeOnPeriodInsideAnotherPeriod_ShouldBeFalse()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(new DatePeriod(
            new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented)
      });

      var isFree = carSchedule.IsFreeOnPeriod(new DatePeriod(
        new DateTimeOffset(2016, 3, 1, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2016, 4, 1, 0, 0, 0, TimeSpan.Zero)));

      Assert.IsFalse(isFree);
    }

    [TestMethod]
    public void IsFreeOnPeriodNotCrossingWithAnotherPeriods_ShouldBeTrue()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented),
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2016, 6, 2, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented)
      });

      var isFree = carSchedule.IsFreeOnPeriod(new DatePeriod(
        new DateTimeOffset(2017, 3, 2, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero)));

      Assert.IsTrue(isFree);
    }

    [TestMethod]
    public void IsFreeOnPeriodBetweenTwoPeriods_ShouldBeTrue()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented),
        new CarOccupation(
          new DatePeriod(
            new DateTimeOffset(2017, 3, 2, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented)
      });

      var isFree = carSchedule.IsFreeOnPeriod(new DatePeriod(
        new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
        new DateTimeOffset(2016, 10, 5, 0, 0, 0, TimeSpan.Zero)));

      Assert.IsTrue(isFree);
    }

    [TestMethod]
    public void ScheduleOccupation_ShouldAddOccupation()
    {
      var carSchedule = new CarSchedule(new CarOccupation[0]);
      var carOccupation = new CarOccupation(
        new DatePeriod(
          new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
          new DateTimeOffset(2016, 10, 5, 0, 0, 0, TimeSpan.Zero)),
        OccupationStatus.Rented);

      carSchedule.ScheduleOccupation(carOccupation);

      CollectionAssert.Contains(carSchedule.Occupations, carOccupation);
    }

    [TestMethod]
    public void GetLastCheckupOfNewCar_ShouldReturnNull()
    {
      var carSchedule = new CarSchedule(new CarOccupation[0]);

      var lastCheckup = carSchedule.GetLastOccupationOfType(OccupationStatus.OnCheckUp);

      Assert.IsNull(lastCheckup);
    }

    [TestMethod]
    public void GetLastRent_ShouldReturnLastRent()
    {
      var carSchedule = new CarSchedule(new[]
      {
        new CarOccupation(new DatePeriod(
            new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2016, 10, 5, 0, 0, 0, TimeSpan.Zero)),
          OccupationStatus.Rented)
      });

      var lastRent = carSchedule.GetLastOccupationOfType(OccupationStatus.Rented);

      Assert.IsNotNull(lastRent);
    }
  }
}