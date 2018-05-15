using lab_4_Controllers_Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_Controllers_Filter.ViewModels
{
    public class AircraftIndexViewModel
    {
        public IEnumerable<Aircraft> Aircraft { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public AirportFilterViewModel AirportFilterViewModel { get; set; }
        public AircraftSortViewModel AircraftSortViewModel { get; set; }
    }
}
