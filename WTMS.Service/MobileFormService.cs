using System;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;
using WTMS.DataAccess.DomainModel;

namespace WTMS.Service
{
    public class MobileFormService
    {
        public bool RegisterInterest(BookingModel bookingModel)
        {
            // Log booking model
            LogHelper.Log(typeof(MobileFormService), bookingModel.ToString());

            using (var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var salesUserId = 0;
                    if (bookingModel.SalesId != null && bookingModel.SalesId.HasValue)
                    {
                        var salesUser = dbContext.systemusers.FirstOrDefault(s => s.id == bookingModel.SalesId);
                        if (salesUser != null)
                        {
                            salesUserId = salesUser.id;
                        }
                    }

                    // Check if phone number exists or not, if not, Save parent info
                    var existingParent = dbContext.parents.FirstOrDefault(p => p.mobile == bookingModel.ParentPhone);
                    var parentId = 0;
                    if (existingParent == null)
                    {
                        var newParent = dbContext.parents.Add(new parent
                        {
                            mobile = bookingModel.ParentPhone,
                            isActive = true,
                            statusId = 1
                        });
                        parentId = newParent.id;
                    } else
                    {
                        parentId = existingParent.id;
                    }

                    // Save child info
                    var newChild = dbContext.children.Add(new child
                    {
                        name = bookingModel.BabyName,
                        birthMonth = bookingModel.BirthMonth,
                        birthYear = bookingModel.BirthYear
                    });

                    // Save child and parent relationship
                    dbContext.childParentRels.Add(new childParentRel
                    {
                        childId = newChild.id,
                        parentId = parentId,
                    });

                    // Save sales record info
                    if (salesUserId != 0)
                    {
                        dbContext.saleshistories.Add(new saleshistory
                        {
                            userId = bookingModel.SalesId.Value,
                            childId = newChild.id
                        });
                    }
                    dbContext.SaveChanges();
                } catch(Exception exc)
                {
                    LogHelper.Log(typeof(MobileFormService), exc.StackTrace);
                }
                
                return true;
            }
        }
    }
}
