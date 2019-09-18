using System;
using System.Collections.Generic;
using PicoPlacaPredictor.Model;

namespace PicoPlacaPredictor.Services.Repository
{
    public class DrivingDataRepository : IDrivingDataRepository
    {
        #region Stub data
        // NO-DRIVE DAYS
        private NoDriveDay Monday => new NoDriveDay { DayName = "MONDAY" };
        private NoDriveDay Tuesday => new NoDriveDay { DayName = "TUESDAY" };
        private NoDriveDay Wednesday => new NoDriveDay { DayName = "WEDNESDAY" };
        private NoDriveDay Thursday => new NoDriveDay { DayName = "THURSDAY" };
        private NoDriveDay Friday => new NoDriveDay { DayName = "FRIDAY" };

        // NO-DRIVE HOURS
        private readonly List<NoDriveTime> NoDriveTimes = new List<NoDriveTime>
        {
            new NoDriveTime {
                StartHour = DateTime.ParseExact("07:00:00", "HH:mm:ss",null),
                EndHour = DateTime.ParseExact("09:00:00", "HH:mm:ss",null) },
            new NoDriveTime {
                StartHour = DateTime.ParseExact("16:00:00", "HH:mm:ss",null),
                EndHour = DateTime.ParseExact("19:30:00", "HH:mm:ss",null) }
        };

        #endregion

        public IEnumerable<DrivingRestriction> LoadDrivingRestrictions()
        {
            var noDriveTimes = new List<NoDriveTime> { NoDriveTimes[0], NoDriveTimes[1] };

            return new List<DrivingRestriction> {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = Monday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "2",
                    NoDriveDay = Monday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "3",
                    NoDriveDay = Tuesday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "4",
                    NoDriveDay = Tuesday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "5",
                    NoDriveDay = Wednesday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "6",
                    NoDriveDay = Wednesday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "7",
                    NoDriveDay = Thursday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "8",
                    NoDriveDay = Thursday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "9",
                    NoDriveDay = Friday,
                    NoDriveTimes = noDriveTimes
                },
                new DrivingRestriction {
                    PlateNumber = "0",
                    NoDriveDay = Friday,
                    NoDriveTimes = noDriveTimes
                }
            };
        }
    }
}
