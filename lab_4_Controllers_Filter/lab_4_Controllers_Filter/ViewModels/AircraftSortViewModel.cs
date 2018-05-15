using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_Controllers_Filter.ViewModels
{
    public enum AircraftSortState
    {
        MarkAsc,
        MarkDesc,
        CapasityAsc,
        CapasityDesc,
        CarryingAsc,
        CarryingDesc
    }

    public class AircraftSortViewModel
    {
        public AircraftSortState MarkSort { get; private set; }
        public AircraftSortState CapasitySort { get; private set; }
        public AircraftSortState CarryingSort { get; private set; }
        public AircraftSortState Current { get; private set; }

        public AircraftSortViewModel(AircraftSortState sortOrder)
        {
            Current = sortOrder;
            MarkSort = sortOrder == AircraftSortState.MarkAsc ? AircraftSortState.MarkDesc : AircraftSortState.MarkAsc;
            CapasitySort = sortOrder == AircraftSortState.CapasityAsc ? AircraftSortState.CapasityDesc : AircraftSortState.CapasityAsc;
            CarryingSort = sortOrder == AircraftSortState.CarryingAsc ? AircraftSortState.CarryingDesc : AircraftSortState.CarryingAsc;
        }
    }
}
