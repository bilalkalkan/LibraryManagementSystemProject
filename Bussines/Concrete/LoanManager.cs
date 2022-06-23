using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

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
        public IDataResult<List<LoanDto>> GetAll()
        {
            var result = _loanDal.GetAll().Any();
            if (result == false)
            {
                return new ErrorDataResult<List<LoanDto>>(Messages.NotFoundList);
            }

            return new SuccessDataResult<List<LoanDto>>(_loanDal.GetLoans());
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



        [SecuredOperation("librarian")]
        public IResult AddLoan(Loan loan)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IResult result = BusinessRules.Run(BookStatusControl(loan.BookId));
                        if (result != null)
                        {
                            return result;
                        }

                        _loanDal.Add(loan);
                        if (loan.LoanDate != null)
                        {
                            Book updateBook = _bookService.GetById(loan.BookId).Data;
                            updateBook.BookStatus = false;
                            _bookService.Update(updateBook);

                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return new SuccessResult(Messages.LoanAdded);

                }
            }

        }

        [SecuredOperation("librarian")]
        public IResult UpdateLoan(Loan loan)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IResult result = BusinessRules.Run(SelectDateControl(loan));
                        if (result != null)
                        {
                            return result;
                        }
                        _loanDal.Update(loan);
                        if (loan.ReturnDate != null)
                        {
                            Book updateBook = _bookService.GetById(loan.BookId).Data;
                            updateBook.BookStatus = true;
                            _bookService.Update(updateBook);

                            transaction.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return new SuccessResult(Messages.LoanUpdated);
        }

        [SecuredOperation("librarian")]
        public IResult DeleteLoan(Loan loan)
        {
            _loanDal.Delete(loan);
            return new SuccessResult(Messages.LoanDeleted);
        }


        private IResult BookStatusControl(int bookId)
        {
            var result = _bookService.GetById(bookId);
            if (result.Data.BookStatus == true)
            {
                return new SuccessResult();
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
