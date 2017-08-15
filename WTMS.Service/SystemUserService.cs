using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;
using WTMS.DataAccess.DomainModel;
using WTMS.DataAccess.ViewModel;

namespace WTMS.Service
{
    public class SystemUserService
    {
        public SystemUserModel Login(LoginViewModel loginModel)
        {
            using (var dbContext = new ntwtmsEntities())
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

        public List<systemuser> GetSalesList()
        {
            using (var dbContext = new ntwtmsEntities())
            {
                return dbContext.systemusers.Where(s => s.wtmsrole.rolename == "sales" && s.isActive == true).ToList();
            }
        }

        public List<ParentViewModel> GetParentList()
        {
            using (var dbContext = new ntwtmsEntities())
            {
                try
                {

                    var resultSet = from p in dbContext.parents
                                    join cp in dbContext.childParentRels on p.id equals cp.parentId
                                    join c in dbContext.children on cp.childId equals c.id
                                    where p.isActive == true
                                    select new
                                    {
                                        p,
                                        cp,
                                        c
                                    };

                    var groupSet = resultSet.GroupBy(r => r.p).Select(r => new ParentViewModel
                    {
                        Parent = r.Key,
                        Children = r.Select(rr => rr.c)
                    });

                    return groupSet.ToList();
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(SystemUserService), e.StackTrace);
                }
                return null;
            }
        }
    }
}
