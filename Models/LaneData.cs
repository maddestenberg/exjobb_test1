using ProtoBuf;
using System.Text.Json.Serialization;

namespace exjobb_test1.Models;

[ProtoContract]
public class LaneData
{
    [ProtoMember(1)]
    [JsonPropertyName("features")]
    public List<StreetNameData> features { get; set; } = new();
}