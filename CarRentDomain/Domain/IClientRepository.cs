namespace CarRent.Domain
{
	public interface IClientRepository
	{
		Client[] LoadClients();
		void SaveClients(Client[] clients);
		void SaveClient(Client client);
	}
}