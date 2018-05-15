using lab_5_ASP_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5_ASP_MVC.ViewModels
{
    public class AircraftIndexViewModel
    {
        public IEnumerable<Aircraft> Aircraft { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public AirportFilterViewModel AirportFilterViewModel { get; set; }
        public AircraftSortViewModel AircraftSortViewModel { get; set; }
    }
}
