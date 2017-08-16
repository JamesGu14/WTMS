using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTMS.DataAccess.ViewModel
{
    public class UserViewModel
    {
    }

    public class ParentViewModel
    {
        public parent Parent { get; set; }
        public IEnumerable<child> Children { get; set; }
        public IEnumerable<userStatu> UserStatus { get; set; }
    }

    public class ParentListViewModel
    {
        public List<ParentViewModel> Parents;
        public List<userStatu> UserStatus;
    }
}
