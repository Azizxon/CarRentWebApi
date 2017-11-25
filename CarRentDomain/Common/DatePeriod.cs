using System;

namespace CarRent.Common
{
  public struct DatePeriod
  {
    public DatePeriod(DateTimeOffset from, DateTimeOffset to)
    {
      From = CutTime(from);
      To = CutTime(to);
      if (From > To)
      {
        throw new ArgumentException("From date should be less, than to date");
      }
    }

    public DateTimeOffset From { get; }

    public DateTimeOffset To { get; }

    public bool DoesIntersectWith(DatePeriod another)
    {
      return (another.From >= From && another.From < To) 
        || (another.To >= From && another.To < To);
    }

    public bool IsLaterThan(DatePeriod another)
    {
      return !DoesIntersectWith(another) && From > another.To;
    }

    private static DateTimeOffset CutTime(DateTimeOffset dateTime)
    {
      return new DateTimeOffset(
        dateTime.Year,
        dateTime.Month,
        dateTime.Day,
        0,
        0,
        0,
        dateTime.Offset);
    }
  }
}