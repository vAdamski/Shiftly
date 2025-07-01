using MediatR;
using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}