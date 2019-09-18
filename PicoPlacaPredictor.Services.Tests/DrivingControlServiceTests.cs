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
        Mock<IDrivingDataRepository> _repositoryMock;

        public DrivingControlServiceTests()
        {
            _repositoryMock = new Mock<IDrivingDataRepository>();
        }

       
        [Fact]
        public void CanDrive_ShouldReturnTrue_WhenPlateNumberIsNotInDrivingRestrictions()
        {
            var plateNumber = "XXXX-XXX";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"},
                    NoDriveTimes = new List<NoDriveTime>()
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);   
           
            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDrive(plateNumber, null, null);

            Assert.True(result);
        }

        [Fact]
        public void CanDrive_ShouldReturnTrue_WhenDateIsNotARestrictedDay()
        {
            var plateNumber = "XXXX-XXX1";
            var noMondayDate = "20/09/2019";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveTimes = new List<NoDriveTime>()
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);   
          
            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDrive(plateNumber, noMondayDate, null);

            Assert.True(result);
        }

        [Fact]
        public void CanDrive_ShouldReturnTrue_WhenDateIsARestrictedDayButTimeIsNotARestrictedTime()
        {
            var plateNumber = "XXXX-XXX1";
            var mondayDate = "16/09/2019";
            var noRestrictedTime = "12:00";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveTimes = new List<NoDriveTime> {
                            new NoDriveTime {
                                StartHour = DateTime.ParseExact("07:00:00", "hh:mm:ss",null),
                                EndHour = DateTime.ParseExact("09:30:00", "hh:mm:ss",null) }
                    }
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);   

            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDrive(plateNumber, mondayDate, noRestrictedTime);

            Assert.True(result);
        }

        [Fact]
        public void CanDrive_ShouldReturnFalse_WhenDateIsARestrictedDayAndTimeIsARestrictedTime()
        {
            var plateNumber = "XXXX-XXX1";
            var mondayDate = "16/09/2019";
            var restrictedTime = "08:00";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction {
                    PlateNumber = "1",
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"} ,
                    NoDriveTimes = new List<NoDriveTime> {
                            new NoDriveTime {
                                StartHour = DateTime.ParseExact("07:00:00", "hh:mm:ss",null),
                                EndHour = DateTime.ParseExact("09:30:00", "hh:mm:ss",null) }
                    }
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);

            var service = new DrivingControlService(_repositoryMock.Object);
            var result = service.CanDrive(plateNumber, mondayDate, restrictedTime);

            Assert.False(result);
        }

        [Fact]
        public void CanDrive_ShouldThrowAFormatException_WhenDateIsInvalid()
        {
            var plateNumber = "XXXX-XXX1";
            var invalidDate = "xx/xx/xxxx";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction { PlateNumber = "1" }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);

            var service = new DrivingControlService(_repositoryMock.Object);
            Assert.Throws<FormatException>(() => service.CanDrive(plateNumber, invalidDate, null));
        }

        [Fact]
        public void CanDrive_ShouldThrowAFormatException_WhenTimeIsInvalid()
        {
            var plateNumber = "XXXX-XXX1";
            var date = "16/09/2019";
            var invalidTime = "xx:xx";
            var drivingRestrictions = new List<DrivingRestriction>
            {
                new DrivingRestriction { 
                    PlateNumber = "1",  
                    NoDriveDay = new NoDriveDay{ DayName = "MONDAY"}  
                }
            };
            _repositoryMock.Setup(r => r.LoadDrivingRestrictions()).Returns(drivingRestrictions);

            var service = new DrivingControlService(_repositoryMock.Object);
            Assert.Throws<FormatException>(() => service.CanDrive(plateNumber, date, invalidTime));
        }
    }
}
