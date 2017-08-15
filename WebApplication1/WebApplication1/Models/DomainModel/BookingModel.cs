using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DomainModel
{
    public class BookingModel
    {
        public string BabyName { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public string ParentPhone { get; set; }
        public string School { get; set; }
        public int? SalesId { get; set; }
    }
}