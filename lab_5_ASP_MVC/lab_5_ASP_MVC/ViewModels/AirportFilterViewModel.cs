using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace lab_5_ASP_MVC.ViewModels
{
    public class AirportFilterViewModel
    {
        public SelectList ListMark { get; private set; }
        public string SelectMark { get; private set; }
        public int SelectCapasity { get; private set; }
        public double SelectCarrying { get; private set; }

        public AirportFilterViewModel(List<string> listMark, string mark, int capasity, double carrying)
        {
            listMark.Insert(0, "");
            ListMark = new SelectList(listMark, mark);
            SelectMark = mark;
            SelectCapasity = capasity;
            SelectCarrying = carrying;
        }
    }
}
