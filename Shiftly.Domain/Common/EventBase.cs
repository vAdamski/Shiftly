using Newtonsoft.Json;

namespace Shiftly.Domain.Common;

public abstract class EventBase
{
    [JsonIgnore]
    public abstract Guid StreamId { get; }
    [JsonIgnore]
    public abstract int Version { get; }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}