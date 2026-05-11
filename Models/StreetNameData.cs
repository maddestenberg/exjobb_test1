using ProtoBuf;
using System.Text.Json.Serialization;

namespace exjobb_test1.Models;

[ProtoContract]
public class RootObject
{
    [ProtoMember(1)]
    public ResponseWrapper RESPONSE { get; set; } = new();
}

[ProtoContract]
public class ResponseWrapper
{
    [ProtoMember(1)]
    public List<ResultWrapper> RESULT { get; set; } = new();
}

[ProtoContract]
public class ResultWrapper
{
    [ProtoMember(1)]
    public List<StreetNameData> Gatunamn { get; set; } = new();
}

[ProtoContract]
public class StreetNameData
{
    [ProtoMember(1)] public string? Valid_From { get; set; }
    [ProtoMember(2)] public string? Valid_To { get; set; }
    [ProtoMember(3)] public int GID { get; set; }
    [ProtoMember(4)] public string? Element_Id { get; set; }
    [ProtoMember(5)] public string? Namn { get; set; }
    [ProtoMember(6)] public string? Feature_Oid { get; set; }
    [ProtoMember(7)] public double Start_Measure { get; set; }
    [ProtoMember(8)] public double End_Measure { get; set; }
    [ProtoMember(9)] public int Seq_No { get; set; }
    [ProtoMember(10)] public int Role { get; set; }
    [ProtoMember(11)] public int Direction { get; set; }
    [ProtoMember(12)] public int IsHost { get; set; }
    [ProtoMember(13)] public string? Updated { get; set; }
    [ProtoMember(14)] public bool Deleted { get; set; }
    [ProtoMember(15)] public Geometry? Geometry { get; set; }
    [ProtoMember(16)] public string? ModifiedTime { get; set; }
}

[ProtoContract]
public class Geometry
{
    [ProtoMember(1)]
    [JsonPropertyName("WKT-SWEREF99TM-3D")]
    public string? WKT_SWEREF99TM_3D { get; set; }

    [ProtoMember(2)]
    [JsonPropertyName("WKT-WGS84-3D")]
    public string? WKT_WGS84_3D { get; set; }
}