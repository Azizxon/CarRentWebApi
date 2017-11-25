namespace CarRent.Domain
{
  public interface ICarRepository
  {
    Car[] LoadCars();
    void SaveCars(Car[] cars);
    void SaveCar(Car carToSave);
  }
}
