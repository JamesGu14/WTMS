using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTMS.DataAccess.ViewModel;

namespace WTMS.DataAccess.ModelMapping
{
    public static class ModelMapping
    {
        public static ParentViewModel MappingToViewModel(this parent parent)
        {
            return new ParentViewModel
            {
                Id = parent.id,
                UserName = parent.username,
                Name = parent.name,
                RelationToChild = parent.relationToChild,
                Mobile = parent.mobile,
                Password = parent.password,
                CreatedAt = parent.createdAt,
                IsActive = parent.isActive,
                DeactivatedAt = parent.deactivateAt,
                StatusId = parent.statusId,
                Comment = parent.comment
            };
        }

        public static ChildViewModel MappingToViewModel(this child child)
        {
            return new ChildViewModel
            {
                Id = child.id,
                Name = child.name,
                NickName = child.nickName,
                BirthYear = child.birthYear,
                BirthMonth = child.birthMonth,
                Gender = child.gender,
                Comment = child.comment
            };
        }

        public static List<ChildViewModel> MappingToViewModel(this List<child> child)
        {
            var resultList = new List<ChildViewModel>();
            child.ForEach(c =>
            {
                resultList.Add(c.MappingToViewModel());
            });
            return resultList;
        }

    }
}
