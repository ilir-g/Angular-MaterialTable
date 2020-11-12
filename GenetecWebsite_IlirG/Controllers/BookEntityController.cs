using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenetecDomain_IlirG.Models;
using GenetecService_IlirG;
using GenetecService_IlirG.BookEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GenetecSite_IlirG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookEntityController : ControllerBase
    {
        private readonly ILogger<BookEntityController> _logger;
        private readonly IBookEntity BookEntity;


        public BookEntityController(
            ILogger<BookEntityController> logger,
           IBookEntity BookEntity)
        {
            this._logger = logger;
            this.BookEntity = BookEntity;
        }

        /// <summary>
        /// method to get all book entities
        /// </summary>
        /// <returns>a list of jobs</returns>
        [HttpGet("BookEntitiesList")]
        [ProducesResponseType(typeof(ResponseModel<BookEntities>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookEntitiesList()
        {
            try
            {
                _logger.LogInformation("Init method GetBookEntities::BookEntityController", DateTime.Now);
                var result = await BookEntity.GetBookEntitiesListAsync();
              
                _logger.LogInformation("End method GetBookEntities::BookEntityController", DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: ", ex);              
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to get entity by id
        /// </summary>
        /// <returns>existing entity</returns>
        [HttpGet("GetBookById/{Id}")]
        [ProducesResponseType(typeof(ResponseModel<BookEntities>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookByIdAsync(int? Id)
        {
            try
            {
                
                _logger.LogInformation("Init method GetBookByIdAsync::BookEntityController", DateTime.Now);
                if (!Id.HasValue)
                {
                    return Ok();
                }
                var result = await BookEntity.GetBookByIdAsync(Id.Value);

                _logger.LogInformation("End method GetBookByIdAsync::BookEntityController", DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: ", ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to create new book entity
        /// </summary>
        /// <returns>new book entity saved</returns>
        [HttpPost("CreateBookEntity")]
        [ProducesResponseType(typeof(ResponseModel<BookEntities>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBookEntityAsync(BookEntities book)
        {
            try
            {
                ResponseModel<BookEntities> result;
                _logger.LogInformation("Init method CreateBookEntityAsync::BookEntityController", DateTime.Now);
                if (book.Id > 0)
                {
                    result = await BookEntity.UpdateBookEntityAsync(book);
                }
                else
                {
                    result = await BookEntity.CreateBookEntityAsync(book);
                }
                _logger.LogInformation("End method CreateBookEntityAsync::BookEntityController", DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: ", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
