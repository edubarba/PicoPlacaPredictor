using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PicoPlacaPredictor.Services;

namespace PicoPlacaPredictor.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrivingController : ControllerBase
    {
        private readonly DrivingControlService _drivingControlService;

        public DrivingController(DrivingControlService drivingControlService)
        {
            _drivingControlService = drivingControlService;
        }

        [HttpGet("verify")]
        public ActionResult<string> VerifyDriving(DrivingModel model)
        {
            try 
            {
                var canDrive = _drivingControlService.CanDrive(model.PlateNumber, model.Date, model.Time);
                return (canDrive) 
                    ? $"The plate number '{model.PlateNumber}' CAN be on the road." +
                      $"\n{_drivingControlService.Message}"
                    : $"The plate number '{model.PlateNumber}' CANNOT be on the road." +
                      $"\n{_drivingControlService.Message}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}