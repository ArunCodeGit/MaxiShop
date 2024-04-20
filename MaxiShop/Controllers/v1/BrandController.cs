using MaxiShop.Application.ApplicationConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Exceptions;
using MaxiShop.Application.Services;
using MaxiShop.Application.Services.Interfaces;
using MaxiShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;
        protected readonly APIResponse _response;

        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _response = new APIResponse();
            _logger = logger;

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName = "Default")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var brands = await _brandService.GetAllAsync();
                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;
                _logger.LogInformation("Records Fetched");
            }
            catch (Exception)
            {
                _logger.LogError("Brand Controller Get operation failed to retrieve the data!");
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName = "Default")]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                var Brand = await _brandService.GetByIdAsync(id);

                if (Brand == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.RecordNotFound;

                    return Ok(_response);
                }
                _response.IsSuccess = true;
                _response.Result = Brand;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
            }

            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.AddErrors(ModelState.ToString());
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    return Ok(_response);
                }
                var entity = await _brandService.CreateAsync(dto);

                _response.statusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = entity;
            }
            catch (BadRequestException ex)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddWarnings(ex.Message);
                _response.Result = ex.validationErrors;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
            }

            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName = "Default")]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.AddErrors(ModelState.ToString());
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                    return Ok(_response);
                }

                var brand = await _brandService.GetByIdAsync(dto.Id);

                if (brand == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                    return Ok(_response);
                }
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.UpdateOperationSuccess;
                await _brandService.UpdateAsync(dto);
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
                _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }

                var brand = await _brandService.GetByIdAsync(id);
                if (brand == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }
                await _brandService.DeleteAsync(id);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddErrors(CommonMessage.SystemError);
                _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
            }
            return Ok(_response);
        }
    }
}
