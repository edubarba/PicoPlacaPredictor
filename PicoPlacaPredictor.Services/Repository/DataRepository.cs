using System;
using System.Collections.Generic;
using PicoPlacaPredictor.Model;

namespace PicoPlacaPredictor.Services.Repository
{
    public class DataRepository : IDataRepository
    {
        // NO-DRIVE DAYS
        private NoDriveDay Monday => new NoDriveDay { DayName = "MONDAY" };
        private NoDriveDay Tuesday => new NoDriveDay { DayName = "TUESDAY" };
        private NoDriveDay Wednesday => new NoDriveDay { DayName = "WEDNESDAY" };
        private NoDriveDay Thursday => new NoDriveDay { DayName = "THURSDAY" };
        private NoDriveDay Friday => new NoDriveDay { DayName = "FRIDAY" };

        // NO-DRIVE HOURS
        private readonly List<NoDriveTime> NoDriveHours = new List<NoDriveTime>
        {
            new NoDriveTime {   
                StartHour = DateTime.ParseExact("07:00:00", "hh:mm:ss",null),
                EndHour = DateTime.ParseExact("09:00:00", "hh:mm:ss",null) },
            new NoDriveTime {  
                StartHour = DateTime.ParseExact("16:00:00", "hh:mm:ss",null),
                EndHour = DateTime.ParseExact("19:30:00", "hh:mm:ss",null) }
        };


        public IEnumerable<DrivingRestriction> LoadDrivingSchedule()
        {
            var commonNoDriveHours = new List<NoDriveTime> { NoDriveHours[0], NoDriveHours[1] };

            return new List<DrivingRestriction> {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = Monday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "2",
                    NoDriveDay = Monday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "3",
                    NoDriveDay = Tuesday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "4",
                    NoDriveDay = Tuesday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "5",
                    NoDriveDay = Wednesday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "6",
                    NoDriveDay = Wednesday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "7",
                    NoDriveDay = Thursday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "8",
                    NoDriveDay = Thursday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "9",
                    NoDriveDay = Friday,
                    NoDriveHours = commonNoDriveHours
                },
                new DrivingRestriction {
                    PlateNumber = "0",
                    NoDriveDay = Friday,
                    NoDriveHours = commonNoDriveHours
                }
            };
        }
    }
}
