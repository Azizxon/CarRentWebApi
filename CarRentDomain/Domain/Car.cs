using System;
using CarRent.Common;

namespace CarRent.Domain
{
  public class Car : ICar
  {
    public Car(int id, string model, string color, CarSchedule carSchedule)
    {
      Id = id;
      Model = model;
      Color = color;
      CarSchedule = carSchedule;
    }

    public int Id { get; }
    public string Model { get; }
    public string Color { get; }
    public CarSchedule CarSchedule { get; }

    public void Rent(
      DatePeriod datePeriod, 
      int maxRentsCountWithoutCheckup,
      TimeSpan checkUpTime)
    {
      if (!CarSchedule.IsFreeOnPeriod(datePeriod))
      {
         throw new CarIsUnvailableException(Id,datePeriod);
      }

      var lastCheckup = CarSchedule.GetLastOccupationOfType(OccupationStatus.OnCheckUp);
      if (lastCheckup != null && lastCheckup.Period.IsLaterThan(datePeriod))
      {
          throw new CarIsUnvailableException(Id, datePeriod);
      }

      CarSchedule.ScheduleOccupation(new CarOccupation(datePeriod, OccupationStatus.Rented));
      var rentsAfterPeriod = CountRentsAfterLastCheckup(lastCheckup);
      if (rentsAfterPeriod < maxRentsCountWithoutCheckup)
      {
        return;
      }

      var lastRent = CarSchedule.GetLastOccupationOfType(OccupationStatus.Rented);
      var firstCheckUpDay = lastRent.Period.To.AddDays(1);
      var checkUpPeriod = new DatePeriod(firstCheckUpDay, firstCheckUpDay.Add(checkUpTime));
      CarSchedule.ScheduleOccupation(new CarOccupation(checkUpPeriod, OccupationStatus.OnCheckUp));
    }

    private int CountRentsAfterLastCheckup(CarOccupation lastCheckup)
    {

      var rentsCount = 0;
      if (lastCheckup == null)
      {
        return rentsCount;
      }

      foreach (var occupation in CarSchedule.Occupations)
      {
        if (occupation.OccupationStatus == OccupationStatus.Rented 
          && occupation.Period.IsLaterThan(lastCheckup.Period))
        {
          rentsCount++;
        }
      }

      return rentsCount;
    }
  }
}