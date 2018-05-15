using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5_ASP_MVC.ViewModels
{
    public enum TicketSortState
    {
        FullNameAsc,
        FullNameDesc,
        PassportDataAsc,
        PassportDataDesc,
        NumberPlaceAsc,
        NumberPlaceDesc,
        PriceAsc,
        PriceDesc
    }

    public class TicketSortViewModel
    {
        public TicketSortState FullNameSort { get; private set; }
        public TicketSortState PassportDataSort { get; private set; }
        public TicketSortState NumberPlaceSort { get; private set; }
        public TicketSortState PriceSort { get; private set; }
        public TicketSortState Current { get; private set; }

        public TicketSortViewModel(TicketSortState sortOrder)
        {
            Current = sortOrder;
            FullNameSort = sortOrder == TicketSortState.FullNameAsc ? TicketSortState.FullNameDesc : TicketSortState.FullNameAsc;
            PassportDataSort = sortOrder == TicketSortState.PassportDataAsc ? TicketSortState.PassportDataDesc : TicketSortState.PassportDataAsc;
            NumberPlaceSort = sortOrder == TicketSortState.NumberPlaceAsc ? TicketSortState.NumberPlaceDesc : TicketSortState.NumberPlaceAsc;
            PriceSort = sortOrder == TicketSortState.PriceAsc ? TicketSortState.PriceDesc : TicketSortState.PriceAsc;
        }
    }
}
