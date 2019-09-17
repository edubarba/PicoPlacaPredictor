using System.Collections.Generic;
using PicoPlacaPredictor.Model;

namespace PicoPlacaPredictor.Services.Repository
{
    public interface IDataRepository
    {
        IEnumerable<DrivingRestriction> LoadDrivingSchedule();
    }
}
