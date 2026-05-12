using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using exjobb_test1.Models;
using exjobb_test1.Services;
using System.Diagnostics;
using System.Globalization;

namespace exjobb_test1.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 30)]
public class SerializationBenchmarks
{
    private List<StreetNameData> _data = new();

    private readonly JsonService _jsonService = new();
    private readonly XmlService _xmlService = new();
    private readonly ProtobufService _protobufService = new();

    private byte[] _jsonBytes = Array.Empty<byte>();
    private byte[] _xmlBytes = Array.Empty<byte>();
    private byte[] _protobufBytes = Array.Empty<byte>();

    [GlobalSetup]
    public void Setup()
    {
        _data = DataLoader.LoadFromJson("Data/testdata_test1.json");

        _jsonBytes = _jsonService.Serialize(_data);
        _xmlBytes = _xmlService.Serialize(_data);
        _protobufBytes = _protobufService.Serialize(_data);

        var objectCount = _data.Count;

        Directory.CreateDirectory("Results");

        Console.WriteLine();
        Console.WriteLine("=== OBJECT COUNT ===");
        Console.WriteLine($"Loaded objects: {objectCount:N0}");

        if (_data.Count > 0)
        {
            Console.WriteLine($"First object: {_data[0].Namn}");
        }

        Console.WriteLine();
        Console.WriteLine("=== PAYLOAD SIZE ===");
        Console.WriteLine($"JSON: {_jsonBytes.Length:N0} bytes");
        Console.WriteLine($"XML: {_xmlBytes.Length:N0} bytes");
        Console.WriteLine($"PROTOBUF: {_protobufBytes.Length:N0} bytes");

        var csvLines = new List<string>
        {
            "Method,Iteration,ElapsedTimeMs,CpuTimeMs,PayloadBytes,ObjectCount"
        };

        Console.WriteLine();
        Console.WriteLine("=== RAW 30 ITERATIONS ===");

        MeasureRaw30("Json_Serialize", () => _jsonService.Serialize(_data), _jsonBytes.Length, objectCount, csvLines);
        MeasureRaw30("Xml_Serialize", () => _xmlService.Serialize(_data), _xmlBytes.Length, objectCount, csvLines);
        MeasureRaw30("Protobuf_Serialize", () => _protobufService.Serialize(_data), _protobufBytes.Length, objectCount, csvLines);

        MeasureRaw30("Json_Deserialize", () => _jsonService.Deserialize(_jsonBytes), _jsonBytes.Length, objectCount, csvLines);
        MeasureRaw30("Xml_Deserialize", () => _xmlService.Deserialize(_xmlBytes), _xmlBytes.Length, objectCount, csvLines);
        MeasureRaw30("Protobuf_Deserialize", () => _protobufService.Deserialize(_protobufBytes), _protobufBytes.Length, objectCount, csvLines);

       var resultPath = "/Users/mads/Desktop/SYSTEMVET/T6 SYSTEMVET/examensarbete/exjobb_test1/Results/raw-iterations.csv";

Directory.CreateDirectory(Path.GetDirectoryName(resultPath)!);

File.WriteAllLines(resultPath, csvLines);

Console.WriteLine($"Saved CSV to: {resultPath}");

        File.WriteAllLines(resultPath, csvLines);

        Console.WriteLine($"Saved CSV to: {resultPath}");
    }


    private static void MeasureRaw30(
        string methodName,
        Func<object?> action,
        int payloadBytes,
        int objectCount,
        List<string> csvLines)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {methodName} ---");

        for (int i = 1; i <= 30; i++)
        {
            var process = Process.GetCurrentProcess();

            var cpuBefore = process.TotalProcessorTime;
            var stopwatch = Stopwatch.StartNew();

            var result = action();

            stopwatch.Stop();
            var cpuAfter = process.TotalProcessorTime;

            GC.KeepAlive(result);

            double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            double cpuMs = (cpuAfter - cpuBefore).TotalMilliseconds;

            Console.WriteLine(
                $"Iteration {i}: elapsed={elapsedMs.ToString("F6", CultureInfo.InvariantCulture)} ms, cpu={cpuMs.ToString("F6", CultureInfo.InvariantCulture)} ms"
            );

            csvLines.Add(
                $"{methodName},{i},{elapsedMs.ToString("F6", CultureInfo.InvariantCulture)},{cpuMs.ToString("F6", CultureInfo.InvariantCulture)},{payloadBytes},{objectCount}"
            );
        }
    }

    [Benchmark]
    public byte[] Json_Serialize()
    {
        return _jsonService.Serialize(_data);
    }

    [Benchmark]
    public byte[] Xml_Serialize()
    {
        return _xmlService.Serialize(_data);
    }

    [Benchmark]
    public byte[] Protobuf_Serialize()
    {
        return _protobufService.Serialize(_data);
    }

    [Benchmark]
    public List<StreetNameData>? Json_Deserialize()
    {
        return _jsonService.Deserialize(_jsonBytes);
    }

    [Benchmark]
    public List<StreetNameData>? Xml_Deserialize()
    {
        return _xmlService.Deserialize(_xmlBytes);
    }

    [Benchmark]
    public List<StreetNameData>? Protobuf_Deserialize()
    {
        return _protobufService.Deserialize(_protobufBytes);
    }
}