using AutoMapper;
using DddInPractice.Api.Models;
using DddInPractice.CommandHandlers.Commands;
using DddInPractice.Commands.Commands;
using DddInPractice.QueryHandlers;
using DddInPractice.QueryHandlers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DddInPractice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakMachineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISnackTypesQueries _snackTypesQueries;

        public SnakMachineController(IMediator mediator,
            IMapper mapper, ISnackTypesQueries snackTypesQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _snackTypesQueries = snackTypesQueries;
        }

        [Route("initialize")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InitializeSnakMachine(
            [FromBody] InitializeSnakMachineDto model)
        {
            var command = _mapper.Map<InitializeSnakMachineCommand>(model);
            command.UserId = null;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id:int}/addSlot")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSlot(
            [FromRoute] int id,
            [FromBody] AddSnackMachineSlotDto model)
        {
            var command = _mapper.Map<AddSnackMachineSlotCommand>(model);
            command.UserId = null;
            command.SnackMachineId = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id:int}/buySnack")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSlot(
            [FromRoute] int id,            
            [FromBody] BuySnackDto model)
        {
            var command = _mapper.Map<BuySnackCommand>(model);
            command.UserId = null;            
            command.SnackMachineId = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("snackTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SnackTypes>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetSnackTypes()
        {
            var snackTypes = await _snackTypesQueries.GetSnackTypes();
            return Ok(snackTypes);
        }
    }
}