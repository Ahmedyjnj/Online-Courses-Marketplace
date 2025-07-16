using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ModelBase<T>
    {
        public T Id { get; set; }

        public ModelBase()
        {
            if (typeof(T) == typeof(Guid))
            {
                Id = (T)(object)Guid.NewGuid();
            }
        }
    }

}
