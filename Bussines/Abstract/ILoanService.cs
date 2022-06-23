using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ILoanService
    {
        IDataResult<List<LoanDto>> GetAll();
        IDataResult<Loan> GetByBookId(int bookId);
        IDataResult<Loan> GetById(int id);
        IResult AddLoan(Loan loan);
        IResult UpdateLoan(Loan loan);
        IResult DeleteLoan(Loan loan);
    }
}
