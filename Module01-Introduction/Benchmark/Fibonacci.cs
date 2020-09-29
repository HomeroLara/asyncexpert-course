using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [AsciiDocExporter]
    [CsvExporter]
    [HtmlExporter]
    [JsonExporter]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            var values = new ulong[n + 1];
            values[0] = 0;
            values[1] = 1;

            return RecursiveCalculation(n, values);
        }

        protected ulong RecursiveCalculation(ulong n, ulong[] values)
        {
            var val = RecursiveCalculation(n - 1, values) + RecursiveCalculation(n - 2, values);
            values[n] = val;

            return val;
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            ulong val1 = 1;
            ulong val2 = 2;

            for ( ulong nXi=0; nXi<n; nXi++ )
            {
                ulong val3 = val1;
                val1 = val2;
                val2 = val3 + val2;
            }

            return val1;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
