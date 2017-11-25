namespace CarRent.Infrastructure
{
	public interface IIdProvider
	{
		int GetNewCarId();
		int GetNewClientId();
		int GetNewRentId();
	}
}