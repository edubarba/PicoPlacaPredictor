using System.Collections.Generic;
using PicoPlacaPredictor.Model;

namespace PicoPlacaPredictor.Services.Repository
{
    public interface IDrivingDataRepository
    {
        IEnumerable<DrivingRestriction> LoadDrivingRestrictions();
    }
}