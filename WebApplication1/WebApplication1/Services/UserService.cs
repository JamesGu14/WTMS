using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService
    {
        public List<systemuser> GetSalesList()
        {
            using(var dbContext = new wtmsEntities())
            {
                return dbContext.systemusers.Where(s => s.wtmsrole.rolename == "sales" && s.isActive == 1).ToList();
            }
        }
    }
}