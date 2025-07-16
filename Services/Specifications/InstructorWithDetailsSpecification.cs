//using Domain.Models.Instructors;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Specifications
//{
//    public class InstructorWithDetailsSpecification : BaseSpecification<Instructor, Guid>
//    {
//        public InstructorWithDetailsSpecification(Guid? id = null, bool includeCourses = false) //false due to performance
//            : base(id.HasValue ? i => i.Id == id : null)
//        {
//            if (includeCourses)
//            {
//                AddInclude(i => i.InstructorCourses);
//                AddInclude(i => i.InstructorCourses.Select(ic => ic.Course));
//            }
//        }
//    }
//}
