using CarRent.Common;
using CarRent.Domain;

namespace CarRent.Application
{
	public interface IClientCarRental
	{
		Car[] ListAvailableCars(int clientId, DatePeriod period);
		int RegisterClient(string name, Credentials credentials);
		void RentCar(int clientId, DatePeriod period, int carId);
	    IClient FindBy(Credentials credentials);
	}
}