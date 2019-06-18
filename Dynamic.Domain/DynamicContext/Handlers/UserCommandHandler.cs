using System;
using System.Net;
using Dynamic.Domain.DynamicContext.Commands;
using Dynamic.Domain.DynamicContext.Commands.Users;
using Dynamic.Domain.DynamicContext.Entities;
using Dynamic.Domain.DynamicContext.Repositories;
using Dynamic.Domain.DynamicContext.ValueObjects;
using Dynamic.Shared.DynamicContext.Commands;
using Dynamic.Shared.DynamicContext.Enums;
using Dynamic.Shared.DynamicContext.Extensions;
using Dynamic.Shared.DynamicContext.Handlers;
using FluentValidator;

namespace Dynamic.Domain.DynamicContext.Handlers
{
    public class UserCommandHandler : Notifiable,
                                        ICommandHandler<GetUserCommand, GetUserCommandResult>,
                                        ICommandHandler<CreateUserCommand, CreateUserCommandResult>,
                                        ICommandHandler<UpdateUserCommand, CommandResult>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUserCommandResult Handle(GetUserCommand command)
        {
            GetUserCommandResult result = new GetUserCommandResult();

            try
            {
                if (command.Id == null)
                    AddNotification(nameof(command.Id).Beautify(), ENotifications.Null.Description());

                if (Valid)
                {
                    User user = _userRepository.GetById();

                    if (user != null)
                        result = new GetUserCommandResult(HttpStatusCode.OK).Build<User, GetUserCommandResult>(user, command.ReturnFields);

                    else
                        result = new GetUserCommandResult(HttpStatusCode.NoContent);
                }

                else
                    result = new GetUserCommandResult(HttpStatusCode.BadRequest, Notifications);
            }
            catch (Exception)
            {
                //Log the error
            }

            return result;
        }

        public CreateUserCommandResult Handle(CreateUserCommand command)
        {
            CreateUserCommandResult result = new CreateUserCommandResult();

            try
            {
                User user = new User(command.Username, new Email(command.Email), new Password(command.Password));

                AddNotifications(user.Notifications);

                if (Valid)
                {
                    _userRepository.Add(user);

                    if (_userRepository.Valid)
                        result = new CreateUserCommandResult(HttpStatusCode.OK).Build<User, CreateUserCommandResult>(user);
                }

                else
                    result = new CreateUserCommandResult(HttpStatusCode.BadRequest, Notifications);
            }
            catch (Exception)
            {
                //Log the error
            }

            return result;
        }

        public CommandResult Handle(UpdateUserCommand command)
        {
            CommandResult result = new CommandResult();

            try
            {
                if (command.Id == null)
                    AddNotification(nameof(command.Id).Beautify(), ENotifications.Null.Description());

                if (Valid)
                {
                    User user = _userRepository.GetById(command.Id);

                    if (user != null)
                    {
                        user.Update(command.Username == null ? user.Username : command.Username,
                                    command.Email == null ? user.Email : new Email(command.Email),
                                    command.Password == null ? user.Password : new Password(command.Password));

                        AddNotifications(user.Notifications);

                        if (Valid)
                        {
                            _userRepository.Update(user);

                            if (_userRepository.Valid)
                                result = new CommandResult(HttpStatusCode.OK);
                        }

                        else
                            result = new CommandResult(HttpStatusCode.BadRequest, Notifications);
                    }

                    else
                        result = new CommandResult(HttpStatusCode.NoContent);
                }

                else
                    result = new CommandResult(HttpStatusCode.BadRequest, Notifications);
            }
            catch (Exception)
            {
                //Log the error
            }

            return result;
        }
    }
}