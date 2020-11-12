using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenetecDomain_IlirG.Models;
using GenetecService_IlirG;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GenetecWebsite_IlirG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookHistoryController : ControllerBase
    {
        private readonly ILogger<BookHistoryController> _logger;
        private readonly IBookHistory BookHistory;


        public BookHistoryController(
            ILogger<BookHistoryController> logger,
           IBookHistory BookHistory)
        {
            this._logger = logger;
            this.BookHistory = BookHistory;
        }

        /// <summary>
        /// method to get all book history
        /// </summary>
        /// <returns>a list of object history</returns>
        [HttpGet("BookHistoryList")]
        [ProducesResponseType(typeof(IEnumerable<History>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookHistoryList()
        {
            try
            {
                _logger.LogInformation("Init method GetBookHistoryList::HistoryController", DateTime.Now);
                var result = await BookHistory.GetBookHistoryListAsync();

                _logger.LogInformation("End method GetBookHistoryList::HistoryController", DateTime.Now);
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
