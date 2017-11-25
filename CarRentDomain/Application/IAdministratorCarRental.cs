using CarRent.Domain;

namespace CarRent.Application
{
	public interface IAdministratorCarRental
	{
		Car[] ListAllCars();
		void AddNewCar(string model, string color);
	}
}