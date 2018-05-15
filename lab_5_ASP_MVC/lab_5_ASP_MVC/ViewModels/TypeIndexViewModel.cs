using System.Collections.Generic;

namespace lab_5_ASP_MVC.ViewModels
{
    public class TypeIndexViewModel
    {
        public IEnumerable<Models.Type> Types { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TypeFilterViewModel TypeFilterViewModel { get; set; }
        public TypeSortViewModel TypeSortViewModel { get; set; }
    }
}
