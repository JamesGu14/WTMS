using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTMS.DataAccess;
using WTMS.DataAccess.DomainModel;
using WTMS.DataAccess.ViewModel;

namespace WTMS.Service
{
    public class SystemUserService
    {
        public SystemUserModel Login(LoginViewModel loginModel)
        {
            using(var dbContext = new ntwtmsEntities())
            {
                var systemUser = dbContext.systemusers
                    .Where(s => s.username == loginModel.UserName && s.password == loginModel.Password)
                    .Include(s => s.wtmsrole)
                    .Select(s => new SystemUserModel
                {
                    UserName = s.username,
                    Name = s.name,
                    WtmsRole = s.wtmsrole.rolename,
                    CreatedAt = s.createdAt.Value,
                    IsActive = s.isActive.Value,
                    Comment = s.comment
                }).FirstOrDefault();

                if (systemUser != null)
                {
                    return systemUser;
                }
            }
            return null;
        }
    }
}
