using System.Text.Json;
using exjobb_test1.Models;

namespace exjobb_test1.Services;

public class JsonService
{
    public byte[] Serialize(List<StreetNameData> data)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }

    public List<StreetNameData>? Deserialize(byte[] bytes)
    {
        return JsonSerializer.Deserialize<List<StreetNameData>>(bytes);
    }
}