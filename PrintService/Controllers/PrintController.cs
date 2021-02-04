using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrintService.Api.ViewModels;
using PrintService.Domain.Dtos;
using PrintService.Infrastructure.Redis;

namespace PrintService.Api.Controllers
{
    [ApiController]
    public class PrintController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRedisSequence<PrintAtTaskDto> _requestsQueue;

        public PrintController(IRedisSequence<PrintAtTaskDto> requestsQueue, IMapper mapper)
        {
            _mapper = mapper;
            _requestsQueue = requestsQueue;
        }
        /// <summary>
        /// Method receives requests and publish them to service broker
        /// </summary>
        /// <returns></returns>
        [HttpGet("printMeAt")]
        public ActionResult Get([FromQuery]PrintAtRequestViewModel request)
        {
            var requestDto = _mapper.Map<PrintAtTaskDto>(request);
            _requestsQueue.Add(requestDto);
            return Ok();
        }
    }
}
