using System;
using System.Collections.Generic;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;

namespace WTMS.Service
{
    public class ReferenceDataService
    {
        public List<userstatu> GetAllUserStatus()
        {
            using(var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var resultList = dbContext.userstatus.ToList();
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
