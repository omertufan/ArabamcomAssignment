using ArabamcomAssignment.Entities;
using ArabamcomAssignment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using ArabamcomAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ArabamcomAssignment.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {

        private readonly IAdvertService _service;
        private readonly ILogger<AdvertController> _logger;

        public AdvertController(IAdvertService service, ILogger<AdvertController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        // GET: advert/all
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AdvertPagingResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AdvertPagingResponse>> All([FromQuery] AdvertPagingRequest request)
        {

            try
            {
                var adverts = await _service.GetAdverts(request);

                if (adverts.Adverts.Count == 0)
                {
                    _logger.LogError($"Adverts not found.");
                    return NoContent();
                }

                return Ok(adverts);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET advert/get/{id}
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AdvertResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AdvertResponse>> Get(int id)
        {
            try
            {

                var advert = await _service.GetAdvert(id);

                if (advert == null)
                {
                    _logger.LogError($"Advert with id: {id}, not found.");
                    return NoContent();
                }

                return Ok(advert);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // POST advert/visit
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AdvertVisitRequest), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> Visit(AdvertVisitRequest request)
        {
            try
            {
                await _service.AddAdvertVisit(request.AdvertId, HttpContext.Connection.RemoteIpAddress.ToString());

                return Created(new Uri($"Get/{request.AdvertId}", UriKind.Relative), request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }


    }
}
