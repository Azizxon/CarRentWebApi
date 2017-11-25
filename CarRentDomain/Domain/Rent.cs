using CarRent.Common;

namespace CarRent.Domain
{
  public class Rent
  {
    public Rent(int id, int clientId, int carId, DatePeriod period)
    {
      Id = id;
      ClientId = clientId;
      CarId = carId;
      Period = period;
    }

    public int Id { get; }

    public int ClientId { get; }

    public int CarId { get; }

    public DatePeriod Period { get; }
  }
}