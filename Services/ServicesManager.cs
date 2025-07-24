using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Services.Services;
using Services.Services.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork,IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IServiceManager
    {
        private readonly Lazy<ICourseService> _lazyCourseService=new Lazy<ICourseService>(()=>new CourseService(unitOfWork,mapper));
        private readonly Lazy<IStudentService> _lazyStudentService = new Lazy<IStudentService>(() => new StudentServices(unitOfWork, mapper));
        private readonly Lazy<IInstructorService> _lazyInstructorService = new Lazy<IInstructorService>(() => new InstructorService(unitOfWork, mapper));
        private readonly Lazy<IPaymentService> _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, mapper));
        private readonly Lazy<IAttachmentServices> _lazyAttachmentService = new Lazy<IAttachmentServices>(() => new AttachmentServices());

        public ICourseService CourseService => _lazyCourseService.Value;
        public IStudentService StudentService => _lazyStudentService.Value;
        public IInstructorService InstructorService => _lazyInstructorService.Value;
        public IPaymentService PaymentService => _lazyPaymentService.Value;

        public IAttachmentServices AttachmentService => _lazyAttachmentService.Value;

    }
}
