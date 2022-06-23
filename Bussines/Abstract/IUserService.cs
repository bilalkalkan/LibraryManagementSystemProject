using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByUserId(int userId);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        //IDataResult<List<UserOperationClaimDetailDto>> GetUserDetail();
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(UserForUpdateDTo userForUpdate);
        IDataResult<User> GetByMail(string email);
    }
}