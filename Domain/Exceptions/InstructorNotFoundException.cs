using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InstructorNotFoundException(Guid id):NotFoundException($"instructor not found with id{id}")
    {
    }
}
