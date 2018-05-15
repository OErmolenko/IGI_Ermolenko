namespace lab_5_ASP_MVC.ViewModels
{
    public enum TypeSortState
    {
        NameTypeAsc,
        NameTypeDesc,
        AppointmentAsc,
        AppointmentDesc,
        RestrictionsAsc,
        RestrictionsDesc
    }

    public class TypeSortViewModel
    {
        public TypeSortState NameType { get; private set; }
        public TypeSortState Appointment { get; private set; }
        public TypeSortState Restrictions { get; private set; }
        public TypeSortState Current { get; private set; }

        public TypeSortViewModel(TypeSortState sortOrder)
        {
            Current = sortOrder;
            NameType = sortOrder == TypeSortState.NameTypeAsc ? TypeSortState.NameTypeDesc : TypeSortState.NameTypeAsc;
            Appointment = sortOrder == TypeSortState.AppointmentAsc ? TypeSortState.AppointmentDesc : TypeSortState.AppointmentAsc;
            Restrictions = sortOrder == TypeSortState.RestrictionsAsc ? TypeSortState.RestrictionsDesc : TypeSortState.RestrictionsAsc;
        }
    }
}
