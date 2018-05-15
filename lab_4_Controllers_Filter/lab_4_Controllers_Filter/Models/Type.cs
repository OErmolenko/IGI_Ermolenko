using System.Collections.Generic;

namespace lab_4_Controllers_Filter.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string NameType { get; set; }
        public string Appointment { get; set; }
        public string Restrictions { get; set; }
        
        public virtual List<Aircraft> Aircraft { get; set; }

        public override string ToString()
        {
            return "Наименование типа: " + NameType + "\nНазначение: " + Appointment + "\nОграничения: " + Restrictions;
        }
    }
}
