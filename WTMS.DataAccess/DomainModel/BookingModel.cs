namespace WTMS.DataAccess.DomainModel
{
    public class BookingModel
    {
        public string BabyName { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public string ParentPhone { get; set; }
        public string School { get; set; }
        public int? SalesId { get; set; }
        public string Gender { get; set; }

        public string ToString()
        {
            return "BabyName: " + BabyName + "; Birth: " + BirthYear + "-" + BirthMonth
                + "; ParentPhone: " + ParentPhone + "; SalesId: " + SalesId + "; Gender: " + Gender;
        }
    }
}