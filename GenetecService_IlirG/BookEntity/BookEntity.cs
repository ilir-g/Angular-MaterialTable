using GenetecDomain_IlirG;
using GenetecDomain_IlirG.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace GenetecService_IlirG.BookEntity
{
    public class BookEntity : GenericRepository<BookEntities>, IBookEntity
    {
        private IBookHistory _bookHistory { get; set; }
        public BookEntity(IBookHistory bookHistory, Genetec_IlirGContext context):base(context)
        {
            _bookHistory = bookHistory;
        }
        public async Task<ResponseModel<IEnumerable<BookEntities>>> GetBookEntitiesListAsync()
        {
            var response = new ResponseModel<IEnumerable<BookEntities>>();
            try
            {
                response.Item = await base.GetListAsync();
                response.HasError = false;
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseModel<BookEntities>> CreateBookEntityAsync(BookEntities book)
        {
            var response = new ResponseModel<BookEntities>();
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
        public async Task<ResponseModel<BookEntities>> UpdateBookEntityAsync(BookEntities book)
        {
            var response = new ResponseModel<BookEntities>();
            try
            {
                var oldEntity = await base.GetByIdAsync(book.Id);
                var listBookHistory = new List<History>();
                if (oldEntity.Title != book.Title)
                {
                    var bookHistory = new History
                    {
                        DateChanged = DateTime.Now,
                        Description = $"Title was changed to {book.Title}",
                        EntityBookId = book.Id
                    };
                    listBookHistory.Add(bookHistory);                  
                }

                if (oldEntity.Description != book.Description)
                {
                    var bookHistory = new History
                    {
                        DateChanged = DateTime.Now,
                        Description = $"Description was changed to {book.Description}",
                        EntityBookId = book.Id
                    };
                    listBookHistory.Add(bookHistory);
                }
                if (oldEntity.Authors != book.Authors)
                {
                    var bookHistory = new History
                    {
                        DateChanged = DateTime.Now,
                        Description = $"Authors was changed to {book.Authors}",
                        EntityBookId = book.Id
                    };
                    listBookHistory.Add(bookHistory);
                   
                }
                if(listBookHistory.Count > 0)
                {
                    var result = await _bookHistory.CreateRangeBookHistoryAsync(listBookHistory);

                    if (result.HasError)
                    {
                        response.HasError = true;
                        response.Message = result.Message;
                        return response;
                    }
                }
                book.PublishDate = oldEntity.PublishDate;

                context.Entry(oldEntity).CurrentValues.SetValues(book);
                await context.SaveChangesAsync();
                response.HasError = false;
                response.Item = book;
               
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseModel<BookEntities>> GetBookByIdAsync(int Id)
        {
            var response = new ResponseModel<BookEntities>();
            try
            {
                response.HasError = false;
                response.Item = await base.GetByIdAsync(Id);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;              
            }
            return response;
        }
    }
}
