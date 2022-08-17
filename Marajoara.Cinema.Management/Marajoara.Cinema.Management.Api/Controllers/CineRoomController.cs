﻿using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CineRoomController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;

        public CineRoomController(ILogger<CineRoomController> logger)
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllCineRoomsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCineRoomCommand AddCineRoomCommand)
        {
            return HandleResult(await _mediator.Send(AddCineRoomCommand));
        }

        [HttpDelete("ByName/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            return HandleResult(await _mediator.Send(new DeleteCineRoomCommand(name)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteCineRoomCommand(id)));
        }
    }
}
