using System;
using System.Collections.Generic;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;
using WTMS.DataAccess.ViewModel;

namespace WTMS.Service
{
    public class ClientUserService
    {
        public List<ParentViewModel> GetParentList()
        {
            using (var dbContext = new ntwtmsEntities())
            {
                try
                {

                    var resultSet = (from p in dbContext.parents
                                     join cp in dbContext.childParentRels on p.id equals cp.parentId
                                     join c in dbContext.children on cp.childId equals c.id
                                     where p.isActive == true
                                     select new { p, cp, c }).ToList();

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
