namespace BookFx.Benchmark
{
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<ClassPropsBm>();
            BenchmarkRunner.Run<StackVsListBm>();
        }
    }
}
