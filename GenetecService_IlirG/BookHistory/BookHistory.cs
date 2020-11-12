using GenetecDomain_IlirG;
using GenetecDomain_IlirG.Models;
using GenetecService_IlirG.BookEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenetecService_IlirG
{
    public class BookHistory : GenericRepository<History>, IBookHistory
    {
        public BookHistory(Genetec_IlirGContext context) : base(context)
        {
            
        }
        public async Task<ResponseModel<History>> CreateBookHistoryAsync(History book)
        {
            var response = new ResponseModel<History>();
            try
            {
                response.HasError = false;
                response.Item = await base.InsertAsync(book);               
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseModel<IEnumerable<History>>> CreateRangeBookHistoryAsync(IEnumerable<History> bookList)
        {
            var response = new ResponseModel<IEnumerable<History>>();
            try
            {
                response.HasError = false;
                response.Item = await base.InsertRangeAsync(bookList);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseModel<IEnumerable<History>>> GetBookHistoryListAsync()
        {
            var response = new ResponseModel<IEnumerable<History>>();
            try
            {
                response.HasError = false;
                response.Item= await base.GetListAsync();
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message=ex.Message;
            }
            return response;
        }
    }
}
