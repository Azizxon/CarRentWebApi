using CarRent.Common;

namespace CarRent.Domain
{
  public class CarOccupation
  {
    public CarOccupation(DatePeriod period, OccupationStatus occupationStatus)
    {
      Period = period;
      OccupationStatus = occupationStatus;
    }

    public DatePeriod Period { get; }

    public OccupationStatus OccupationStatus { get; }
  }
}