using BenchmarkDotNet.Attributes;

[InProcess()]
[MemoryDiagnoser]
[MinColumn, MaxColumn, AllStatisticsColumn]
[GcServer(true)]
public class ChunkingBenchmark
{
    private int[] _data;
    private int _chunkSize = 1000;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = Enumerable.Range(1,10000000).ToArray();
    }

    [Benchmark]
    public void Span()
    {
        var total = 0;
        var dataAsSpan = _data.AsSpan();
        for (int i = 0; i < dataAsSpan.Length; i += _chunkSize)
        {
            var chunk = dataAsSpan.Slice(i, Math.Min(_chunkSize, _data.Length - i));
            total += chunk.Length;
        }
    }

    [Benchmark]
    public void Chunk()
    {
        var total = 0;
        foreach(var chunk in _data.Chunk(_chunkSize))
        {
            total += chunk.Length;
        }
    }

    [Benchmark]
    public void SkipAndTake()
    {
        var total = 0;
        for(int i = 0; i < _data.Length; i += _chunkSize)
        {
            var chunk = _data.Skip(i).Take(Math.Min(_chunkSize, _data.Length - i));
            total += chunk.Count();
        }
    }
}