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

    public class ParentChildrenViewModel
    {
        public ParentViewModel Parent { get; set; }
        public IEnumerable<ChildViewModel> Children { get; set; }
        public IEnumerable<userstatu> UserStatus { get; set; }
    }

    public class ParentListViewModel
    {
        public List<ParentChildrenViewModel> Parents;
        public List<userstatu> UserStatus;
    }

    public class ParentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RelationToChild { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public int StatusId { get; set; }
        public string Comment { get; set; }
    }

    public class ChildViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int? BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public string Gender { get; set; }
        public string Comment { get; set; }
    }

}
