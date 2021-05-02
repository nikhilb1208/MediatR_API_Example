using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatR_API_Example.Features.User
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => this._mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("Users", Name = "Get Users")]
        [ProducesResponseType(typeof(IReadOnlyList<User>),200)]
        public async Task<ActionResult<IReadOnlyList<User>>> Get()
        {
            var result = await _mediator.Send(new GetUsers.Query());

            return Ok(result);
        }
    }
}