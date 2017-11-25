using System;

namespace CarRent
{
  public class CarRentSettings
  {
    public CarRentSettings(int maxRentsWithoutCheckup, TimeSpan checkupTime)
    {
      MaxRentsWithoutCheckup = maxRentsWithoutCheckup;
      CheckupTime = checkupTime;
    }

    public int MaxRentsWithoutCheckup { get; }

    public TimeSpan CheckupTime { get; }
  }
}