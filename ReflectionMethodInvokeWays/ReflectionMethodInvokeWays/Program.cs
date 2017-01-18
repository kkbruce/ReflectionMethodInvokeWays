using System;
using System.Diagnostics;
using BenchmarkDotNet.Running;

namespace ReflectionMethodInvokeWays
{
    class Program
    {
        static void Main(string[] args)
        {
            // 功能驗證
            //FunctionValid();
            //Console.Read();

            // Stopwatch Pattern
            RunStopwatch();
            Console.Read();


            //!+ Change to Release Mode
            //RunBenchmark();
            //Console.Read();
            //Console.Read();

        }

        private static void RunStopwatch()
        {
            object myClass = new MyClass();
            var mi = new MethodInvoke();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way1_DirectMethodCall(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way1 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way2_CreateLambdaCall(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way2 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way3_RelectionAPIMethodInvoke(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way3 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way4_Using_dynamic_Keyword(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way4 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way5_CreateDelegateCall(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way5 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way6_CreateExpressionCall(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way6 Timimg: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 20000; i++)
            {
                mi.Way7_EmitAPIDynamicMethod(myClass);
            }
            sw.Stop();
            Console.WriteLine($"Way7 Timimg: {sw.ElapsedMilliseconds} ms");
        }

        private static void RunBenchmark()
        {
            var summary = BenchmarkRunner.Run<MethodInvokeBenchmark>();
        }

        private static void FunctionValid()
        {
            object myClass = new MyClass();
            var mi = new MethodInvoke();
            mi.Way1_DirectMethodCall(myClass);
            mi.Way2_CreateLambdaCall(myClass);
            mi.Way3_RelectionAPIMethodInvoke(myClass);
            mi.Way4_Using_dynamic_Keyword(myClass);
            mi.Way5_CreateDelegateCall(myClass);
            mi.Way6_CreateExpressionCall(myClass);
            mi.Way7_EmitAPIDynamicMethod(myClass);
        }

    }
}
