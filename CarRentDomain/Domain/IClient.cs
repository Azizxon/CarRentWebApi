using CarRent.Common;

namespace CarRent.Domain
{
	public interface IClient
	{
		int Id { get; }
		string Name { get; }
		string Email { get; }
        Credentials Credentials { get; }
		Rent[] Rents { get; }
		void AddNewRent(Rent rent);
		bool CanRentCarOnPeriod(DatePeriod period);
	}
}