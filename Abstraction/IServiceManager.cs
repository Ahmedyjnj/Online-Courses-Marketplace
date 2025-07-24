using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IServiceManager
    {

        ICourseService CourseService { get; }
        IStudentService StudentService { get; }
        IInstructorService InstructorService { get; }
        IPaymentService PaymentService { get; }

        IAttachmentServices AttachmentService { get; }
        
    }
}
