using MediatR;

namespace Common.Application.Queries;

public interface IQuery<out TQueryResult> : IRequest<TQueryResult> { }