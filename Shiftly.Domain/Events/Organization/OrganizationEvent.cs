using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public record OrganizationEvent(Guid StreamId) : Event(StreamId);