using System;
using System.Linq;
using System.Collections.Generic;
using PicoPlacaPredictor.Model;
using PicoPlacaPredictor.Services.Repository;

namespace PicoPlacaPredictor.Services
{
    public class DrivingControlService 
    {
        private readonly IDataRepository _dataRepository;
        private IEnumerable<DrivingRestriction> _drivingRestrictions;

        public DrivingControlService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _drivingRestrictions = _dataRepository.LoadDrivingSchedule();
        }

        public bool CanDriveByDate(string plateNumber, string date, string time)
        {
            var restriction = _drivingRestrictions
                .FirstOrDefault(FilterByPlateNumber(plateNumber));

            if (restriction == null) { return true; }

            return !IsRestrictedDay(restriction.NoDriveDay, date) || 
                !IsRestrictedTime(restriction.NoDriveHours, time);
        }

        private static Func<DrivingRestriction, bool> FilterByPlateNumber(string number)
        {
            return r => number.EndsWith(r.PlateNumber, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsRestrictedDay(NoDriveDay restrictedDay, string inputDate)
        {
            var date = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

            return string.Equals(restrictedDay.DayName, 
                                  date.DayOfWeek.ToString(), 
                                  StringComparison.InvariantCultureIgnoreCase); 
        }

        private static bool IsRestrictedTime(IEnumerable<NoDriveTime> resctritedTime, string inputTime)
        {
            var time = DateTime.ParseExact(inputTime, "hh:mm:ss", null);
            return resctritedTime.Any(
                x => time.TimeOfDay > x.StartHour.TimeOfDay &&
                time.TimeOfDay < x.EndHour.TimeOfDay);
        }

    }
}
