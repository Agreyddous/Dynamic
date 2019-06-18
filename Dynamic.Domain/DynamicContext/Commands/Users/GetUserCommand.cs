using System;
using Dynamic.Shared.DynamicContext.Commands;

namespace Dynamic.Domain.DynamicContext.Commands.Users
{
    public class GetUserCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string ReturnFields { get; private set; }

        public void setId(Guid id) => Id = id;
        public void setReturnFields(string returnFields) => ReturnFields = returnFields;
    }
}