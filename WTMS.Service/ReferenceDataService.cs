using System;
using System.Collections.Generic;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;

namespace WTMS.Service
{
    public class ReferenceDataService
    {
        public List<userStatu> GetAllUserStatus()
        {
            using(var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var resultList = dbContext.userStatus.ToList();
                    return resultList;

                } catch (Exception e)
                {
                    LogHelper.Log(typeof(ReferenceDataService), e);
                }
                return null;
            }
        }
    }
}
