using GenetecDomain_IlirG.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenetecService_IlirG.BookEntity
{
    public interface IBookEntity
    {
        Task<ResponseModel<IEnumerable<BookEntities>>> GetBookEntitiesListAsync();
        Task<ResponseModel<BookEntities>> CreateBookEntityAsync(BookEntities book);
        Task<ResponseModel<BookEntities>> UpdateBookEntityAsync(BookEntities book);
        Task<ResponseModel<BookEntities>> GetBookByIdAsync(int Id);
    }
}
