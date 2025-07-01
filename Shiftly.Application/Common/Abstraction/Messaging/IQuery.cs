using MediatR;
using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}