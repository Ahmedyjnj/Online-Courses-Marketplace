using Shared.Dto_s.ContentDto;
using Shared.Dto_s.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IContentServices
    {
        Task<ContentDto> GetByIdAsync(Guid Contentid);

        Task<IEnumerable<ContentDto>> GetAllAsync(Guid Courseid);
        Task<IEnumerable<ContentDto>> GetAllWithoutAccerssAsync(Guid courseid);


        Task<bool> AddContent(ContentDto content);

        Task<bool> UpdateContent(ContentDto content);

        Task<bool> DeleteContent(Guid Contentid);

        Task<bool> AccessState(Guid CourseId, Guid CurrentUser, string TypePerson);

    }
}
