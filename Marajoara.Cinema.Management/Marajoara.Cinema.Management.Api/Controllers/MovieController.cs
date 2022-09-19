using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Manager")]
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
            return HandleResult(await _mediator.Send(new AllMoviesQuery()));
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMovieCommand updateMovieCommand)
        {
            return HandleResult(await _mediator.Send(updateMovieCommand));
        }

        [HttpPut("{movieID}/Poster")]
        public async Task<IActionResult> UploadPoster(int movieID, IFormFile file)
        {
            return HandleResult(await _mediator.Send(new UpdateMoviePosterCommand(movieID, file.OpenReadStream())));
        }

        [HttpGet("{movieID}/Poster")]
        public async Task<IActionResult> GetPoster(int movieID)
        {
            return HandleResult(await _mediator.Send(new GetMoviePosterQuery(movieID)));
        }

        [HttpDelete("{movieID}/Poster")]
        public async Task<IActionResult> DeletePoster(int movieID)
        {
            return HandleResult(await _mediator.Send(new DeleteMoviePosterCommand(movieID)));
        }
    }
}
