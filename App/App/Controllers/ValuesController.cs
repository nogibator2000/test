using App.InputModels;
using App.Repository;
using App.Services;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private IUserRepository userRepository;
        private IRecordRepository recordRepository;
        private ICodeRepository codeRepository;
        private IReaderService readerService;
        private IRecordsService recordsService;

        public ValuesController(ILogger<ValuesController> logger, IUserRepository userRepository, IRecordRepository recordRepository, ICodeRepository codeRepository, IRecordsService recordsService, IReaderService readerService)
        {
            this.userRepository = userRepository;
            this.recordRepository = recordRepository;
            this.codeRepository = codeRepository;
            this.readerService = readerService;
            this.recordsService = recordsService;
            _logger = logger;
        }

        [Authorize(Roles = "admin")]

        [HttpPost]
        public IActionResult LoadXLS(IFormFile file, int month, int year)
        {
            try
            {
                readerService.LoadExcel(file, month, year);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult LoadAdmins(IFormFile model)
        {
            try
            {
                readerService.LoadAdmins(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll(int month, int year)
        {
            try
            {
                return Ok(recordsService.GenerateAllNewRecords(month, year));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult GetSelf()
        {
            try
            {
                return Ok(recordsService.GenerateAllRecordsByUser(HttpContext.User.Identity.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult GetPrice(int month, int year)
        {
            try
            {
                return Ok(recordsService.GeneratePrice(month, year));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult ChangeStatus(int id)
        {
            try
            {
                recordsService.ChangeStatus(HttpContext.User.Identity.Name, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Close(int month, int year)
        {
            try
            {
                recordsService.Close(HttpContext.User.Identity.Name, month, year);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

