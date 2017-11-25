using CarRent.Domain;
using CarRent.Infrastructure;

namespace CarRent.Application
{
	public class AdministratorCarRental : IAdministratorCarRental
	{
		public AdministratorCarRental(ICarRepository carFileRepository, IIdProvider idProvider)
		{
			_carFileRepository = carFileRepository;
			_idProvider = idProvider;
		}

		public Car[] ListAllCars()
		{
			var cars = _carFileRepository.LoadCars();
			return cars;
		}

		public void AddNewCar(string model, string color)
		{
			var newCarId = _idProvider.GetNewCarId();
			var newCar = new Car(newCarId, model, color, new CarSchedule(new CarOccupation[0]));
			_carFileRepository.SaveCar(newCar);
		}

		private readonly ICarRepository _carFileRepository;
		private readonly IIdProvider _idProvider;
	}
}