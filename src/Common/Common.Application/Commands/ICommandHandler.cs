using MediatR;

namespace Common.Application.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
{ }