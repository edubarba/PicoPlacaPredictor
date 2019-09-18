using System;
using System.Linq;
using System.Collections.Generic;
using PicoPlacaPredictor.Model;
using PicoPlacaPredictor.Services.Repository;
using System.Text;

namespace PicoPlacaPredictor.Services
{
    public class DrivingControlService
    {
        private const string DateFormat = "dd/MM/yyyy";
        private const string TimeFormat = "HH:mm";
        private IEnumerable<DrivingRestriction> _drivingRestrictions;
        public string Message {get; private set;}


        public DrivingControlService(IDrivingDataRepository repository)
        {
            _drivingRestrictions = repository.LoadDrivingRestrictions();
        }

        public bool CanDrive(string plateNumber, string date, string time)
        {
            var restriction = _drivingRestrictions
                .FirstOrDefault(FilterByPlateNumber(plateNumber));

            if (restriction == null) 
            { 
                Message = $"'{plateNumber}' is not registered for no-drive days.";
                return true; 
            }
            var canDrive = true;
            if (IsRestrictedDay(restriction.NoDriveDay, date))
            {   
                canDrive = (!IsRestrictedTime(restriction.NoDriveTimes, time));
            }

            Message = BuildNoDriveDataMessage(plateNumber, date, restriction);
            return canDrive;
        }

        private static Func<DrivingRestriction, bool> FilterByPlateNumber(string plateNumber)
        {
            return r => plateNumber.EndsWith(r.PlateNumber, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsRestrictedDay(NoDriveDay restrictedDay, string dateToVerify)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(dateToVerify, DateFormat, null);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Invalid format for '{dateToVerify}'. Required format: '{DateFormat}'.", ex);
            }
            
            var dayName = date.DayOfWeek.ToString();
            return string.Equals(restrictedDay.DayName, dayName,StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsRestrictedTime(IEnumerable<NoDriveTime> restrictedTimes, string timeToVerify)
        {
            DateTime time;
            try
            {
                time = DateTime.ParseExact(timeToVerify, TimeFormat, null);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Invalid format for '{timeToVerify}'. Required format: '{TimeFormat}'.",ex);
            }

            return restrictedTimes.Any(x =>
                            time.TimeOfDay > x.StartHour.TimeOfDay &&
                            time.TimeOfDay < x.EndHour.TimeOfDay);
        }

        private static string BuildNoDriveDataMessage(string plateNumber, string date, DrivingRestriction restriction)
        {
            var sb = new StringBuilder();
            sb.Append($"\nNo-drive data for {plateNumber}");
            sb.Append($"\nNo-Drive Date: '{restriction.NoDriveDay.DayName}");
            sb.Append($"\nNo-Drive Time: ");
          
            var values = restriction.NoDriveTimes
                    .Select(t => t.StartHour.ToShortTimeString() + "-"+  t.EndHour.ToShortTimeString());
            sb.Append( string.Join(" ", values));
            
            return sb.ToString();
        }

    }
}
