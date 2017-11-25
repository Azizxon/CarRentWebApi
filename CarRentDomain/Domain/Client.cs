using System.Collections.Generic;
using CarRent.Common;

namespace CarRent.Domain
{
  public class Client : IClient
  {
    public Client(int id, string name, Rent[] rents,Credentials credentials)
    {
      Id = id;
      Name = name;
      Rents = rents;
        Credentials = credentials;
    }

    public int Id { get; }

    public string Name { get; }

    public string Email { get; }

      public Credentials Credentials { get; }

      public Rent[] Rents { get; private set; }

    public void AddNewRent(Rent rent)
    {
      if (!CanRentCarOnPeriod(rent.Period))
      {
            throw new CarIsUnvailableException(rent.CarId,rent.Period);
      }

        var rentsList = new List<Rent>(Rents) {rent};
        Rents = rentsList.ToArray();
    }

    public bool CanRentCarOnPeriod(DatePeriod period)
    {
      foreach (var rent in Rents)
      {
        var intersects = rent.Period.DoesIntersectWith(period);
        if (intersects)
        {
          return false;
        }
      }

      return true;
    } 
  }
}