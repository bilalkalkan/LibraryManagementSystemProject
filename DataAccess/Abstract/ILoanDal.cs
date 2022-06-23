using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ILoanDal:IEntityRepository<Loan>
    {
        List<LoanDto> GetLoans(Expression<Func<LoanDto, bool>> filter = null);
    }
}
