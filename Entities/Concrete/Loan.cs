using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Loan:IEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
