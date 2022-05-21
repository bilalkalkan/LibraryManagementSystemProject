using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ILoanService
    {
        IDataResult<List<Loan>> GetAll();
        IDataResult<Loan> GetByBookId(int bookId);
        IDataResult<Loan> GetById(int id);
        IResult AddLoan(Loan loan);
        IResult UpdateLoan(Loan loan);
        IResult DeleteLoan(Loan loan);
    }
}
