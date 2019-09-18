using System.Collections.Generic;

namespace PicoPlacaPredictor.Model
{
    public class DrivingRestriction
    {
        public string PlateNumber { get; set; }

        public NoDriveDay NoDriveDay { get; set; }

        public IEnumerable<NoDriveTime> NoDriveTimes { get; set; }
    }
}
