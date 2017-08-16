using System;
using System.Collections.Generic;
using System.Linq;
using WTMS.Common;
using WTMS.DataAccess;
using WTMS.DataAccess.ModelMapping;
using WTMS.DataAccess.ViewModel;

namespace WTMS.Service
{
    public class ClientUserService
    {
        public List<ParentChildrenViewModel> GetParentList()
        {
            using (var dbContext = new ntwtmsEntities())
            {
                try
                {

                    var resultSet = (from p in dbContext.parents
                                     join cp in dbContext.childparentrels on p.id equals cp.parentId
                                     join c in dbContext.children on cp.childId equals c.id
                                     where p.isActive == true
                                     select new { p, cp, c }).ToList();

                    var groupSet = resultSet.GroupBy(r => r.p).Select(r => new ParentChildrenViewModel
                    {
                        Parent = r.Key.MappingToViewModel(),
                        Children = r.Select(rr => rr.c).ToList().MappingToViewModel()
                    }).OrderBy(g => g.Parent.StatusId);

                    return groupSet.ToList();
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(SystemUserService), e.StackTrace);
                }
                return null;
            }
        }

        public List<ChildViewModel> GetChildrenByParent(int parentId)
        {
            using (var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var childrenIdList = dbContext.childparentrels.Where(r => r.parentId == parentId).Select(r => r.childId).Distinct().ToList();
                    return dbContext.children.Where(c => childrenIdList.Contains(c.id)).ToList().MappingToViewModel();
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(SystemUserService), e.StackTrace);
                }
                return null;
            }
        }

        public ParentViewModel GetParentById(int parentId)
        {
            using (var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var parent = dbContext.parents.FirstOrDefault(p => p.id == parentId);
                    if (parent != null)
                    {
                        return parent.MappingToViewModel();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(SystemUserService), e.StackTrace);
                }
                return null;
            }
        }

        public bool UpdateParent(ParentViewModel parentViewModel)
        {
            if (parentViewModel == null)
            {
                return false;
            }

            using (var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var existingParent = dbContext.parents.FirstOrDefault(p => p.id == parentViewModel.Id);
                    if (existingParent == null)
                    {
                        LogHelper.Log(typeof(SystemUserService), "Parent does not exist, id = " + parentViewModel.Id);
                        return false;
                    }

                    existingParent.name = parentViewModel.Name;
                    existingParent.relationToChild = parentViewModel.RelationToChild;
                    existingParent.mobile = parentViewModel.Mobile;
                    existingParent.statusId = parentViewModel.StatusId;
                    existingParent.comment = parentViewModel.Comment;

                    dbContext.parents.Attach(existingParent);
                    var entry = dbContext.Entry(existingParent);
                    entry.Property(e => e.name).IsModified = true;
                    entry.Property(e => e.relationToChild).IsModified = true;
                    entry.Property(e => e.mobile).IsModified = true;
                    entry.Property(e => e.statusId).IsModified = true;
                    entry.Property(e => e.comment).IsModified = true;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(ReferenceDataService), e);
                }
                return false;
            }
        }

        public bool UpdateChildList(List<ChildViewModel> childViewModelList)
        {
            var successChild = true;
            foreach(var childViewModel in childViewModelList)
            {
                if (!UpdateChild(childViewModel))
                {
                    successChild = false;
                }
            }
            return successChild;
        }

        public bool UpdateChild(ChildViewModel childViewModel)
        {
            if (childViewModel == null)
            {
                return false;
            }

            using (var dbContext = new ntwtmsEntities())
            {
                try
                {
                    var existingChild = dbContext.children.FirstOrDefault(p => p.id == childViewModel.Id);
                    if (existingChild == null)
                    {
                        LogHelper.Log(typeof(SystemUserService), "Child does not exist, id = " + childViewModel.Id);
                        return false;
                    }

                    existingChild.name = childViewModel.Name;

                    dbContext.children.Attach(existingChild);
                    var entry = dbContext.Entry(existingChild);
                    entry.Property(e => e.name).IsModified = true;
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    LogHelper.Log(typeof(ReferenceDataService), e);
                }
                return false;
            }
        }
    }
}
