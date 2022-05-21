using Autofac;
using Business.Abstract;
using Business.Concrete;
using Bussines.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentManager>().As<IStudentService>().SingleInstance();
            builder.RegisterType<EfStudentDal>().As<IStudentDal>().SingleInstance();

            builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();
            builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();

            builder.RegisterType<LoanManager>().As<ILoanService>().SingleInstance();
            builder.RegisterType<EfLoanDal>().As<ILoanDal>().SingleInstance();
        }
    }
}
