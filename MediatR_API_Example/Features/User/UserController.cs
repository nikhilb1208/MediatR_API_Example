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
        [ProducesResponseType(typeof(IReadOnlyList<User>), 200)]
        public async Task<ActionResult<IReadOnlyList<User>>> Get()
        {
            var result = await _mediator.Send(new GetUsers.Query());

            return Ok(result);
        }

        /// <summary>
        /// Add User Record
        /// </summary>
        /// <param name="command">Add User Command</param>
        /// <returns>Added User Record</returns>
        [HttpPost("User", Name = "Add User Record")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult<User>> Create(
            [FromBody] AddUser.Command command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Update User Record
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="command">Update User Parameters</param>
        /// <returns>Updated User Record</returns>
        [HttpPut("User/{id}", Name = "Update User Record")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult<User>> Update(
            [FromRoute] int id,
            [FromBody] UpdateUser.Command command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Update User Record
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="command">Delete User Parameters</param>
        /// <returns>Delete User Record</returns>
        [HttpDelete("User/{id}", Name = "Delete User Record")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult<User>> Delete(
            [FromRoute] int id,
            [FromBody] DeleteUser.Command command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}