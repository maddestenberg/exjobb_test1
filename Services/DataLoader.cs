using System.Text.Json;
using exjobb_test1.Models;

namespace exjobb_test1.Services;

public static class DataLoader
{
    public static List<StreetNameData> LoadFromJson(string path)
    {
        var json = File.ReadAllText(path);

        var root = JsonSerializer.Deserialize<RootObject>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return root!
            .RESPONSE
            .RESULT
            .SelectMany(r => r.Gatunamn)
            .ToList();
    }
}