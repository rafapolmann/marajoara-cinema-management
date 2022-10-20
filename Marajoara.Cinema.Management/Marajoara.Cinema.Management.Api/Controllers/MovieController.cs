using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MovieController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetMovieQuery(id)));
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("ByTitle/{title}")]
        public async Task<IActionResult> Get(string title)
        {
            return HandleResult(await _mediator.Send(new GetMovieQuery(title)));
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllMoviesQuery()));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpGet("InTheater/{initialDate}/{finalDate}")]
        public async Task<IActionResult> Get(DateTime initialDate, DateTime finalDate)
        {
            return HandleResult(await _mediator.Send(new GetMoviesBySessionDateQuery(initialDate, finalDate)));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpGet("InTheater/{movieID}/{initialDate}/{finalDate}")]
        public async Task<IActionResult> Get(int movieID, DateTime initialDate, DateTime finalDate)
        {
            return HandleResult(await _mediator.Send(new GetMovieWithSessionsByDateQuery(movieID, initialDate, finalDate)));
        }

        [Authorize(Roles = "Manager")]

        [HttpDelete("ByTitle/{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            return HandleResult(await _mediator.Send(new DeleteMovieCommand(title)));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteMovieCommand(id)));
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddMovieCommand addMovieCommand)
        {
            return HandleResult(await _mediator.Send(addMovieCommand));
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMovieCommand updateMovieCommand)
        {
            return HandleResult(await _mediator.Send(updateMovieCommand));
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPut("{movieID}/Poster")]
        public async Task<IActionResult> UploadPoster(int movieID, IFormFile file)
        {
            return HandleResult(await _mediator.Send(new UpdateMoviePosterCommand(movieID, file.OpenReadStream())));
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{movieID}/Poster")]
        public async Task<IActionResult> GetPoster(int movieID)
        {
            return HandleResult(await _mediator.Send(new GetMoviePosterQuery(movieID)));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{movieID}/Poster")]
        public async Task<IActionResult> DeletePoster(int movieID)
        {
            return HandleResult(await _mediator.Send(new DeleteMoviePosterCommand(movieID)));
        }
    }
}
