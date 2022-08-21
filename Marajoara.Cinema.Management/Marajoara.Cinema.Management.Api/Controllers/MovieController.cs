﻿using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetMovieQuery(id)));
        }

        [HttpGet("ByTitle/{title}")]
        public async Task<IActionResult> Get(string title)
        {
            return HandleResult(await _mediator.Send(new GetMovieQuery(title)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new AllMoviesQuery()));
        }

        [HttpDelete("ByTitle/{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            return HandleResult(await _mediator.Send(new DeleteMovieCommand(title)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteMovieCommand(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddMovieCommand addMovieCommand)
        {
            return HandleResult(await _mediator.Send(addMovieCommand));
        }
    }
}