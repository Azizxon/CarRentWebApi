using System.Collections.Generic;
using System.IO;
using CarRent.Domain;
using Newtonsoft.Json;

namespace CarRent.Infrastructure
{
  public class ClientRepository : IClientRepository
  {
    public ClientRepository(string filePath)
    {
      _filePath = filePath;
    }

    public Client[] LoadClients()
    {
      try
      {
        var rawFiles = File.ReadAllText(_filePath);
        var clients = JsonConvert.DeserializeObject<Client[]>(rawFiles);
        return clients;
      }
      catch (FileNotFoundException)
      {
        return new Client[0];
      }
    }

    public void SaveClients(Client[] clients)
    {
      var serialized = JsonConvert.SerializeObject(clients);
      File.WriteAllText(_filePath, serialized);
    }

    public void SaveClient(Client client)
    {
      var clients = LoadClients();
      int? indexOfChangedClient = null;
      for (var index = 0; index < clients.Length; index++)
      {
        if (clients[index].Id == client.Id)
        {
          indexOfChangedClient = index;
        }
      }

      if (indexOfChangedClient.HasValue)
      {
        clients[indexOfChangedClient.Value] = client;
      }
      else
      {
        var carsList = new List<Client>(clients);
        carsList.Add(client);
        clients = carsList.ToArray();
      }

      SaveClients(clients);
    }

    private readonly string _filePath;
  }
}