using System;
using System.Collections.Generic;
using CarRent.Common;

namespace CarRent.Domain
{
  public class CarSchedule : ICarSchedule
  {
    public CarSchedule(CarOccupation[] occupations)
    {
      _occupations = new List<CarOccupation>(occupations);
    }

    public CarOccupation[] Occupations
    {
      get
      {
        return _occupations.ToArray();
      }
    }

    public bool IsFreeOnPeriod(DatePeriod period)
    {
      var busy = false;
      foreach (var occupation in _occupations)
      {
        if (occupation.Period.DoesIntersectWith(period))
        {
          busy = true;
          break;
        }
      }

      return !busy;
    }

    public void ScheduleOccupation(CarOccupation occupation)
    {
      if (!IsFreeOnPeriod(occupation.Period))
      {
        throw new CarIsUnvailableException("Can not schedule occupation on busy period");
      }

      _occupations.Add(occupation);
    }

    public CarOccupation GetLastOccupationOfType(OccupationStatus occupationStatus)
    {
      CarOccupation lastOccupation = null;
      foreach (var occupation in _occupations)
      {
        if (occupation.OccupationStatus == occupationStatus)
        {
          lastOccupation = occupation;
        }
      }

      return lastOccupation;
    }

    private readonly List<CarOccupation> _occupations;
  }
}