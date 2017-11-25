using System.Collections.Generic;
using System.IO;
using CarRent.Domain;
using Newtonsoft.Json;

namespace CarRent.Infrastructure
{
    public class CarFileRepository : ICarRepository
  {
    public CarFileRepository(string filePath)
    {
      _filePath = filePath;
    }

    public Car[] LoadCars()
    {
      try
      {
        var rawFiles = File.ReadAllText(_filePath);
        var cars = JsonConvert.DeserializeObject<Car[]>(rawFiles);
        return cars;
      }
      catch (FileNotFoundException)
      {
        return new Car[0];
      }
    }

    public void SaveCars(Car[] cars)
    {
      var serialized = JsonConvert.SerializeObject(cars);
      File.WriteAllText(_filePath, serialized);
    }

    public void SaveCar(Car carToSave)
    {
      var cars = LoadCars();
      int? indexOfChangedCar = null;
      for (var index = 0; index < cars.Length; index++)
      {
        if (cars[index].Id == carToSave.Id)
        {
          indexOfChangedCar = index;
        }
      }

      if (indexOfChangedCar.HasValue)
      {
        cars[indexOfChangedCar.Value] = carToSave;
      }
      else
      {
        var carsList = new List<Car>(cars);
        carsList.Add(carToSave);
        cars = carsList.ToArray();
      }

      SaveCars(cars);
    }

    private readonly string _filePath;
  }
}