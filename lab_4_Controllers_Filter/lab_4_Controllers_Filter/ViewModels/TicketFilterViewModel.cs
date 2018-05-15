using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_Controllers_Filter.ViewModels
{
    public class TicketFilterViewModel
    {
        public string SelectFullName { get; private set; }
        public string SelectPassportData { get; private set; }

        public TicketFilterViewModel(string fullName, string passportData)
        {
            SelectFullName = fullName;
            SelectPassportData = passportData;
        }
    }
}
