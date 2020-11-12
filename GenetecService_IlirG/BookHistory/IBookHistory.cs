using GenetecDomain_IlirG.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenetecService_IlirG
{
    public interface IBookHistory
    {
        Task<ResponseModel<IEnumerable<History>>> GetBookHistoryListAsync();
        Task<ResponseModel<History>> CreateBookHistoryAsync(History book);
        Task<ResponseModel<IEnumerable<History>>> CreateRangeBookHistoryAsync(IEnumerable<History> bookList);
    }
}
