using MaxiShop.Application.ApplicationConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.Services.Interfaces;
using MaxiShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        protected readonly APIResponse _response;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var brands = _brandService.GetAllAsync();
                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
            }
            return Ok(_response);
        }
    }
}
