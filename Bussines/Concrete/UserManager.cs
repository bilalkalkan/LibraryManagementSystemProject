using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System.Collections.Generic;
using Business.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        //public IDataResult<List<UserOperationClaimDetailDto>> GetUserDetail()
        //{
        //    return new SuccessDataResult<List<UserOperationClaimDetailDto>>(_userDal.GetUserDetail());
        //}

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == userId));
        }

        public IResult Delete(User user)
        {

            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IResult Update(UserForUpdateDTo userForUpdate)
        {
            User userUpdate = _userDal.Get(u => u.Id == userForUpdate.Id);
            if (userUpdate == null)
            {
                return new ErrorResult();
            }

            userUpdate.Id = userForUpdate.Id;
            userUpdate.FirstName = userForUpdate.FirstName;
            userUpdate.LastName = userForUpdate.LastName;
            userUpdate.Email = userForUpdate.Email;

            _userDal.Update(userUpdate);
            return new SuccessResult(Messages.UserUpdated);
        }

    }
}
