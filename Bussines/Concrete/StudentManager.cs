using System.Collections.Generic;
using System.Linq;
using Business.Constants;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public IDataResult<List<Student>> GetAll()
        {
            var result = _studentDal.GetAll().Any();
            if (result == false)
            {
                return new ErrorDataResult<List<Student>>(Messages.NotFoundList);
            }
            return new SuccessDataResult<List<Student>>(_studentDal.GetAll());
        }

        public IDataResult<Student> GetById(int id)
        {
            var result = _studentDal.Get(s => s.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Student>(Messages.NotFound);
            }

            return new SuccessDataResult<Student>(_studentDal.Get(s => s.Id == id));
        }


        [SecuredOperation("librarian")]
        public IResult Add(Student student)
        {
           _studentDal.Add(student);
            return new SuccessResult(Messages.StudentAdded);

        }

        [SecuredOperation("librarian")]
        public IResult Update(Student student)
        {
            _studentDal.Update(student);
            return new SuccessResult(Messages.StudentUpdated);
        }

        [SecuredOperation("librarian")]
        public IResult Delete(Student student)
        {
            _studentDal.Delete(student);
            return new SuccessResult(Messages.StudentDeleted);
        }
    }
}
