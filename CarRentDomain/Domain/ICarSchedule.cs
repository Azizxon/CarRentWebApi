using CarRent.Common;

namespace CarRent.Domain
{
	public interface ICarSchedule
	{
		CarOccupation[] Occupations { get; }
		bool IsFreeOnPeriod(DatePeriod period);
		void ScheduleOccupation(CarOccupation occupation);
		CarOccupation GetLastOccupationOfType(OccupationStatus occupationStatus);
	}
}