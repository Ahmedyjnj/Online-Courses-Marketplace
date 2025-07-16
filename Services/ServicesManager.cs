using Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager : IServiceManager
    {
        public ICourseService CourseService => throw new NotImplementedException();

        public IStudentService StudentService => throw new NotImplementedException();

        public IInstructorService InstructorService => throw new NotImplementedException();

        public IPaymentService PaymentService => throw new NotImplementedException();
    }
}
