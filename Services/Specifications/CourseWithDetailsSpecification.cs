//using Domain.Models.Courses;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Specifications
//{
//    public class CourseWithDetailsSpecification : BaseSpecification<Course, Guid>
//    {
//        public CourseWithDetailsSpecification(Guid? id = null, bool includeStudents = false, bool includeInstructors = false)
//        : base(id.HasValue ? c => c.Id == id : null)
//        {
//            if (includeStudents)
//            {
//                AddInclude(c => c.Enrollments);
//                AddInclude(c => c.Enrollments.Select(e => e.Student));
//            }

//            if (includeInstructors)
//            {
//                AddInclude(c => c.InstructorCourses);
//                AddInclude(c => c.InstructorCourses.Select(ic => ic.Instructor));
//            }
//        }
//    }
//}
