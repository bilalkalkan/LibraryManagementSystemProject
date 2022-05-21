using System.Collections.Generic;
using System.Linq;
using Business.Constants;
using Bussines.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BookManager:IBookService
    {
        private readonly IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public IDataResult<List<Book>> GetAll()
        {
            var result = _bookDal.GetAll().Any();
            if (result==false)
            {
                return new ErrorDataResult<List<Book>>(Messages.NotFoundList);
            }

            return new SuccessDataResult<List<Book>>(_bookDal.GetAll());
        }

        public IDataResult<Book> GetById(int id)
        {
            var result = _bookDal.Get(b => b.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<Book>(Messages.NotFound);
            }

            return new SuccessDataResult<Book>(_bookDal.Get(b => b.Id == id));
        }

        public IResult Add(Book book)
        {

            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }

        public IResult Update(Book book)
        {
            
                _bookDal.Update(book);
                return new SuccessResult(Messages.BookUpdated);
        }

        public IResult Deleted(Book book)
        {
            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }


    }
}
