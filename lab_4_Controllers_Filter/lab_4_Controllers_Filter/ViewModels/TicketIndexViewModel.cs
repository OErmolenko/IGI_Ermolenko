using lab_4_Controllers_Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_Controllers_Filter.ViewModels
{
    public class TicketIndexViewModel
    {
        public IEnumerable<Ticket> Ticket { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TicketFilterViewModel TicketFilterViewModel { get; set; }
        public TicketSortViewModel TicketSortViewModel { get; set; }
    }
}
