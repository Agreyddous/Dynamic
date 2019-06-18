using Dynamic.Shared.DynamicContext.Commands;

namespace Dynamic.Shared.DynamicContext.Handlers
{
    public interface ICommandHandler<T, E> where T: ICommand where E: ICommandResult
    {
        E Handle(T command);
    }
}