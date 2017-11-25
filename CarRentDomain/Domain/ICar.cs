using System;
using CarRent.Common;

namespace CarRent.Domain
{
	public interface ICar
	{
		int Id { get; }
		string Model { get; }
		string Color { get; }
		CarSchedule CarSchedule { get; }

		void Rent(
			DatePeriod datePeriod, 
			int maxRentsCountWithoutCheckup,
			TimeSpan checkUpTime);
	}
}