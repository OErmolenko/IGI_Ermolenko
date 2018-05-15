using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace lab_4_Controllers_Filter.ViewModels
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
