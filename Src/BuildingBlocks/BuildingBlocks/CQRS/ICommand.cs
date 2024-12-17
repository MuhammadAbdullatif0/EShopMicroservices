using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand : IRequest<uint>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
