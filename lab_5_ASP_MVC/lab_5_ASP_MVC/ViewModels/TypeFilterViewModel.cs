using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace lab_5_ASP_MVC.ViewModels
{
    public class TypeFilterViewModel
    {
        public string SelectNameType { get; private set; }

        public TypeFilterViewModel(string nameType)
        {
            SelectNameType = nameType;
        }
    }
}
