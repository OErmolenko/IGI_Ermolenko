using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5_ASP_MVC.ViewModels
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
