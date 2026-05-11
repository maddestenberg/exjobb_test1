using System.Xml.Serialization;
using exjobb_test1.Models;

namespace exjobb_test1.Services;

public class XmlService
{
    private readonly XmlSerializer _serializer = new(typeof(List<StreetNameData>));

    public byte[] Serialize(List<StreetNameData> data)
    {
        using var stream = new MemoryStream();
        _serializer.Serialize(stream, data);
        return stream.ToArray();
    }

    public List<StreetNameData>? Deserialize(byte[] bytes)
    {
        using var stream = new MemoryStream(bytes);
        return _serializer.Deserialize(stream) as List<StreetNameData>;
    }
}