using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTMS.DataAccess.DomainModel
{
    public class SystemUserModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string WtmsRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } 
        public string Comment { get; set; }
    }
}
