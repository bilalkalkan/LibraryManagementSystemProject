using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        //List<UserOperationClaimDetailDto> GetUserDetail(
        //    Expression<Func<UserOperationClaimDetailDto, bool>> filter = null);
    }
}
