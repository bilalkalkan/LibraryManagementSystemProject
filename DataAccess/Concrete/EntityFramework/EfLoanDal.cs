using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLoanDal:EfEntityRepositoryBase<Loan,Context>,ILoanDal
    {
        public List<LoanDto> GetLoans(Expression<Func<LoanDto, bool>> filter = null)
        {
            using (Context context=new Context())
            {
                var result = from loan in context.Loans
                    join student in context.Students on loan.StudentId equals student.Id
                    join book in context.Books on loan.BookId equals book.Id
                    select new LoanDto
                    {
                        Id = loan.Id,
                        StudentFirstName = student.FirstName,
                        StudentLastName = student.LastName,
                        BookName = book.Name,
                        LoanDate = loan.LoanDate,
                        ReturnDate = loan.ReturnDate ?? null
                    };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
