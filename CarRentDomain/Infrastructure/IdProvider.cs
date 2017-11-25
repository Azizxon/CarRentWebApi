using System.Linq;

namespace CarRent.Infrastructure
{
  public class IdProvider : IIdProvider
  {
    public static IdProvider CreateIdProvider(
      CarFileRepository carFileRepository, 
      ClientRepository clientRepository)
    {
      var cars = carFileRepository.LoadCars();
      var clients = clientRepository.LoadClients();
	    var lastClientId = 0;
      var lastRentId = 0;
	    var lastCarId = cars.Select(car => car.Id).Concat(new[] {0}).Max();

	    foreach (var client in clients)
      {
        foreach (var rent in client.Rents)
        {
          if (rent.Id > lastRentId)
          {
            lastRentId = rent.Id;
          }
        }

        if (client.Id > lastClientId)
        {
          lastClientId = client.Id;
        }
      }

      return new IdProvider(lastCarId, lastClientId, lastRentId);
    }

    public int GetNewCarId()
    {
      return ++_lastCarId;
    }

    public int GetNewClientId()
    {
      return ++_lastClientId;
    }

    public int GetNewRentId()
    {
      return ++_lastRentId;
    }

    private IdProvider(int lastCarId, int lastClientId, int lastRentId)
    {
      _lastCarId = lastCarId;
      _lastClientId = lastClientId;
      _lastRentId = lastRentId;
    }

    private int _lastCarId;
    private int _lastClientId;
    private int _lastRentId;
  }
}