using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public  class CourseNotFoundException(Guid id):NotFoundException($"Course with id : {id} deleted or not found")
    {
    }
}
