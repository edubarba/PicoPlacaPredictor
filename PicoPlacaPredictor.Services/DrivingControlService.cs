using System;
using System.Linq;
using System.Collections.Generic;
using PicoPlacaPredictor.Model;
using PicoPlacaPredictor.Services.Repository;

namespace PicoPlacaPredictor.Services
{
    public class DrivingControlService
    {
        private const string DateFormat = "dd/MM/yyyy";
        private const string TimeFormat = "hh:mm:ss";
        private IEnumerable<DrivingRestriction> _drivingRestrictions;

        public DrivingControlService(IDrivingDataRepository repository)
        {
            _drivingRestrictions = repository.LoadDrivingRestrictions();
        }

        public bool CanDrive(string plateNumber, string date, string time)
        {
            var restriction = _drivingRestrictions
                .FirstOrDefault(FilterByPlateNumber(plateNumber));

            if (restriction == null) { return true; }

            return !IsRestrictedDay(restriction.NoDriveDay, date) ||
                   !IsRestrictedTime(restriction.NoDriveTimes, time);
        }

        private static Func<DrivingRestriction, bool> FilterByPlateNumber(string plateNumber)
        {
            return r => plateNumber.EndsWith(r.PlateNumber, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsRestrictedDay(NoDriveDay restrictedDay, string dateToVerify)
        {
            var date = DateTime.ParseExact(dateToVerify, DateFormat, null);
            var dayName = date.DayOfWeek.ToString();
            return string.Equals(restrictedDay.DayName, dayName,StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsRestrictedTime(IEnumerable<NoDriveTime> resctritedTimes, string timeToVerify)
        {
            var time = DateTime.ParseExact(timeToVerify, TimeFormat, null);
            return resctritedTimes.Any(x =>
                            time.TimeOfDay > x.StartHour.TimeOfDay &&
                            time.TimeOfDay < x.EndHour.TimeOfDay);
        }

    }
}
