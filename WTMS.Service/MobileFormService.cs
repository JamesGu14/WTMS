using System.Linq;
using WTMS.DataAccess;
using WTMS.DataAccess.DomainModel;

namespace WTMS.Service
{
    public class MobileFormService
    {
        public bool RegisterInterest(BookingModel bookingModel)
        {
            using (var dbContext = new ntwtmsEntities())
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

                // Save parent info
                var newParent = dbContext.parents.Add(new parent
                {
                    mobile = bookingModel.ParentPhone
                });

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
                    parentId = newParent.id
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
                return true;
            }
        }
    }
}
