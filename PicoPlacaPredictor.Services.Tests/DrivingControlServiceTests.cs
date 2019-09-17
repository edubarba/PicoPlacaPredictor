using System;
using System.Collections.Generic;
using Moq;
using PicoPlacaPredictor.Model;
using PicoPlacaPredictor.Services.Repository;
using Xunit;

namespace PicoPlacaPredictor.Services.Tests
{
    public class DrivingControlServiceTests
    {
        Mock<IDataRepository> _repositoryMock;

        public DrivingControlServiceTests()
        {
            _repositoryMock = new Mock<IDataRepository>();
        }

       
        [Fact]
        public void CanDriveByDate_ShouldReturnTrue_WhenPlateNumberIsNotDrivingRestrictions()
        {
            var plateNumber = "XXXX-XXX9";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"},
                    NoDriveHours = new List<NoDriveTime>()
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingSchedule()).Returns(drivingRestrictions);   
           
            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDriveByDate(plateNumber, null, null);

            Assert.True(result);
        }

        [Fact]
        public void CanDriveByDate_ShouldReturnTrue_WhenInputDateDoesNotMatchWithAnyNoDriveDay()
        {
            var plateNumber = "XXXX-XXX1";
            var noModayDate = "20/09/2019";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveHours = new List<NoDriveTime>()
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingSchedule()).Returns(drivingRestrictions);   
          
            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDriveByDate(plateNumber, noModayDate, null);

            Assert.True(result);
        }

        [Fact]
        public void CanDriveByDate_ShouldReturnTrue_WhenInputDateMatchWithAnyNoDriveDay_And_TimeIsOutRangeOfNoDriveHours()
        {
            var plateNumber = "XXXX-XXX1";
            var mondayDate = "16/09/2019";
            var noRestrictedTime = "12:00:00";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveHours = new List<NoDriveTime> {
                            new NoDriveTime {
                                StartHour = DateTime.ParseExact("07:00:00", "hh:mm:ss",null),
                                EndHour = DateTime.ParseExact("09:30:00", "hh:mm:ss",null) }
                    }
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingSchedule()).Returns(drivingRestrictions);   

            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDriveByDate(plateNumber, mondayDate, noRestrictedTime);

            Assert.True(result);
        }

        [Fact]
        public void CanDriveByDate_ShouldReturnFalse_WhenInputDateMatchWithAnyNoDriveDay_And_TimeIsInRangeOfNoDriveHours()
        {
            var plateNumber = "XXXX-XXX1";
            var mondayDate = "16/09/2019";
            var restrictedTime = "08:00:00";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveHours = new List<NoDriveTime> {
                            new NoDriveTime {
                                StartHour = DateTime.ParseExact("07:00:00", "hh:mm:ss",null),
                                EndHour = DateTime.ParseExact("09:30:00", "hh:mm:ss",null) }
                    }
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingSchedule()).Returns(drivingRestrictions);

            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDriveByDate(plateNumber, mondayDate, restrictedTime);

            Assert.False(result);
        }
    }
}
