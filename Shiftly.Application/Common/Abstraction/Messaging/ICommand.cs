using MediatR;
using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Abstraction.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}