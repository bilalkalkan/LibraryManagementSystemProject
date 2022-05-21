using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Bussines.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAll();
        IDataResult<Book> GetById(int id);
        IResult Add(Book book);
        IResult Update(Book book);
        IResult Deleted(Book book);
    }
}
