using Abstraction;
using AutoMapper;
using CloudinaryDotNet;
using Domain.Contracts;
using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Services;
using Services.Services.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(Cloudinary cloudinary,IUnitOfWork unitOfWork,IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration,HttpClient httpClient) : IServiceManager
    {
        private readonly Lazy<ICourseService> _lazyCourseService=new Lazy<ICourseService>(()=>new CourseService(unitOfWork,mapper));
        private readonly Lazy<IStudentService> _lazyStudentService = new Lazy<IStudentService>(() => new StudentServices(unitOfWork, mapper));
        private readonly Lazy<IInstructorService> _lazyInstructorService = new Lazy<IInstructorService>(() => new InstructorService(unitOfWork, mapper));
        private readonly Lazy<IPaymentServices> _lazyPaymentService = new Lazy<IPaymentServices>(() => new PaymentServices(configuration,httpClient,unitOfWork,mapper));
        private readonly Lazy<IAttachmentServices> _lazyAttachmentService = new Lazy<IAttachmentServices>(() => new AttachmentServices(cloudinary));
        private readonly Lazy<IContentServices> _lazyContentService = new Lazy<IContentServices>(() => new ContentServices(unitOfWork,mapper));
        public ICourseService CourseService => _lazyCourseService.Value;
        public IStudentService StudentService => _lazyStudentService.Value;
        public IInstructorService InstructorService => _lazyInstructorService.Value;
        public IPaymentServices PaymentService => _lazyPaymentService.Value;

        public IAttachmentServices AttachmentService => _lazyAttachmentService.Value;

        public IContentServices ContentServices => _lazyContentService.Value;
    }
}
