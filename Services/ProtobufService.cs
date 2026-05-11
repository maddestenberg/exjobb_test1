using ProtoBuf;
using exjobb_test1.Models;

namespace exjobb_test1.Services;

public class ProtobufService
{
    public byte[] Serialize(List<StreetNameData> data)
    {
        using var stream = new MemoryStream();
        Serializer.Serialize(stream, data);
        return stream.ToArray();
    }

    public List<StreetNameData>? Deserialize(byte[] bytes)
    {
        using var stream = new MemoryStream(bytes);
        return Serializer.Deserialize<List<StreetNameData>>(stream);
    }
}