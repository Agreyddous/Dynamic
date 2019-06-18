using System;
using Dynamic.Domain.DynamicContext.Commands;
using Dynamic.Domain.DynamicContext.Commands.Users;
using Dynamic.Domain.DynamicContext.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Dynamic.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserCommandHandler _handler;

        public UsersController(UserCommandHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get User
        /// </summary>
        ///
        /// <param name="user">User Id to retrieve data</param>
        /// <param name="returnFields">A string of fields separeted by ',' with fields desired to be return, all available data will return if left empty</param>
        ///
        /// <response code="200">Returns the User</response>
        /// <response code="204">User not found</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/Users/{user}/")]
        public GetUserCommandResult Get(Guid user, [FromQuery] string returnFields)
        {
            GetUserCommand command = new GetUserCommand();

            command.setId(user);
            command.setReturnFields(returnFields);

            GetUserCommandResult result = _handler.Handle(command);

            HttpContext.Response.StatusCode = (int)result.Code;

            return result;
        }

        /// <summary>
        /// Create User
        /// </summary>
        ///
        /// <param name="command">User data to create</param>
        ///
        /// <response code="200">Returns the User's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/Users")]
        public CreateUserCommandResult Create([FromBody] CreateUserCommand command)
        {
            if (command == null)
                command = new CreateUserCommand();

            CreateUserCommandResult result = _handler.Handle(command);

            HttpContext.Response.StatusCode = (int)result.Code;

            return result;
        }

        /// <summary>
        /// Update User
        /// </summary>
        ///
        /// <param name="user">User Id to update</param>
        /// <param name="command">User data to udpate</param>
        ///
        /// <response code="200">Successfully updated user</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch]
        [Route("V1/Users/{user}")]
        public CommandResult Update(Guid user, [FromBody] UpdateUserCommand command)
        {
            if (command == null)
                command = new UpdateUserCommand();

            command.setId(user);

            CommandResult result = _handler.Handle(command);

            HttpContext.Response.StatusCode = (int)result.Code;

            return result;
        }
    }
}