using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Bussines.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LoanManager : ILoanService
    {
        private readonly ILoanDal _loanDal;
        private readonly IBookService _bookService;

        public LoanManager(ILoanDal loanDal, IBookService bookService)
        {
            _loanDal = loanDal;
            _bookService = bookService;
        }
        public IDataResult<List<Loan>> GetAll()
        {
            var result = _loanDal.GetAll().Any();
            if (result == false)
            {
                return new ErrorDataResult<List<Loan>>(Messages.NotFoundList);
            }

            return new SuccessDataResult<List<Loan>>(_loanDal.GetAll());
        }

        public IDataResult<Loan> GetByBookId(int bookId)
        {
            return new SuccessDataResult<Loan>(_loanDal.Get(x => x.BookId == bookId));
        }

        public IDataResult<Loan> GetById(int id)
        {
            var result = _loanDal.Get(x => x.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Loan>(Messages.NotFound);
            }

            return new SuccessDataResult<Loan>(_loanDal.Get(x => x.Id == id));
        }

        public IResult AddLoan(Loan loan)
        {
            IResult result = BusinessRules.Run( SelectDateControl(loan), BookStatusControl(loan.BookId));
            if (result != null)
            {
                return result;
            }

            _loanDal.Add(loan);
            return new SuccessResult(Messages.LoanAdded);

        }


        public IResult UpdateLoan(Loan loan)
        {
            _loanDal.Update(loan);
            return new SuccessResult(Messages.LoanUpdated);
        }

        public IResult DeleteLoan(Loan loan)
        {
            _loanDal.Delete(loan);
            return new SuccessResult(Messages.LoanDeleted);
        }

        private IResult BookStatusControl(int bookId)
        {
            var result = _bookService.GetById(bookId);
            if (result.Data.BookStatus ==false)
            {
                return new SuccessResult(true);
            }
            else
            {
                return new ErrorResult("Bu kitap şuan emanette");

            }

        }

        private IResult SelectDateControl(Loan loan)
        {
            if (loan.ReturnDate < loan.LoanDate)
            {
                return new ErrorResult(Messages.DateError);
            }

            return new SuccessResult("Başarılı");
        }


    }
}
