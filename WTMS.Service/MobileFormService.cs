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
                        birthYear = bookingModel.BirthYear,
                        gender = bookingModel.Gender
                    });

                    // Save child and parent relationship
                    dbContext.childparentrels.Add(new childparentrel
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

        public int FakeReadingCount(DateTime startDateTime)
        {
            var now = DateTime.Now;
            var timeSpan = now - startDateTime;

            var days = timeSpan.TotalDays;
            var totalCount = 0.0;
            var baseInterval = 0.6;
            var intervalIncreasement = 0.25;
            for (var i = 0; i < days; i ++)
            {
                totalCount += 24 * 6 * baseInterval;
                baseInterval += intervalIncreasement;
            }

            totalCount += (timeSpan.TotalDays - (int)timeSpan.TotalDays) * 24 * 6 * baseInterval;

            return (int)totalCount;
        }

        public int FakeParticipateCount(DateTime startDateTime)
        {
            var now = DateTime.Now;
            var timeSpan = now - startDateTime;

            int days = (int) Math.Ceiling(timeSpan.TotalDays);
            int totalCount = 0;
            int[] dayRegister = new int[] { 10, 6, 5, 5, 4, 6, 4, 4, 3, 1, 0 };

            for (var i = 0; i < days - 1; i++)
            {
                if (i < dayRegister.Count())
                {
                    totalCount += dayRegister[i];
                }
            }

            if (days < dayRegister.Count())
            {
                var todayRegister = dayRegister[days - 1];
                int upTillNowCount = (int) Math.Min((double)(now.Hour - 8) / 12 * todayRegister, todayRegister);
                totalCount += upTillNowCount;
            }

            return totalCount;
        }
    }
}
