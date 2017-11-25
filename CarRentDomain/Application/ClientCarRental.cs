using System;
using System.Linq;
using CarRent.Common;
using CarRent.Domain;
using CarRent.Infrastructure;

namespace CarRent.Application
{
    public class ClientCarRental : IClientCarRental
    {
        public ClientCarRental(
            ICarRepository carRepository,
            IClientRepository clientRepository,
            IIdProvider idProvider,
            CarRentSettings settings)
        {
            _carFileRepository = carRepository;
            _clientRepository = clientRepository;
            _idProvider = idProvider;
            _settings = settings;
        }

        public Car[] ListAvailableCars(int clientId, DatePeriod period)
        {
            var client = FindClient(clientId);
            var cars = _carFileRepository.LoadCars();
            if (client == null)
            {
                return cars;
            }

            if (!client.CanRentCarOnPeriod(period))
            {
                return new Car[0];
            }

            return cars.Where(car => car.CarSchedule.IsFreeOnPeriod(period)).ToArray();
        }

        public int RegisterClient(string name, Credentials credentials)
        {
            var newClientId = _idProvider.GetNewClientId();
            var client = new Client(newClientId, name, new Rent[0], credentials);
            _clientRepository.SaveClient(client);
            return newClientId;
        }

        public void RentCar(int clientId, DatePeriod period, int carId)
        {
            var client = FindClient(clientId);
            if (client == null)
            {
                throw new UserNotFoundException(clientId);
            }

            var car = FindCar(carId);
            if (car == null)
            {
                throw new CarNotFountException(carId);
            }

            var newRentId = _idProvider.GetNewRentId();
            car.Rent(period, _settings.MaxRentsWithoutCheckup, _settings.CheckupTime);
            client.AddNewRent(new Rent(newRentId, clientId, carId, period));

            _carFileRepository.SaveCar(car);
            _clientRepository.SaveClient(client);
        }

        public IClient FindBy(Credentials credentials)
        {

            var result = _clientRepository.LoadClients()
                .FirstOrDefault(client => client.Credentials.Equals(credentials));
            return result;

        }

        private Client FindClient(int clientId)
        {
            var clients = _clientRepository.LoadClients();
            return clients.FirstOrDefault(client => client.Id == clientId);
        }

        private Car FindCar(int carId)
        {
            var cars = _carFileRepository.LoadCars();
            return cars.FirstOrDefault(car => car.Id == carId);
        }

        private readonly ICarRepository _carFileRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdProvider _idProvider;
        private readonly CarRentSettings _settings;
    }
}