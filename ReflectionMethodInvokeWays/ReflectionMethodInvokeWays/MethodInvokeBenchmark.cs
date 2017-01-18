using BenchmarkDotNet.Attributes;

namespace ReflectionMethodInvokeWays
{
    public class MethodInvokeBenchmark
    {
        private readonly object _myClass = new MyClass();
        private readonly MethodInvoke _mi = new MethodInvoke();

        [Benchmark]
        public void Way1() => _mi.Way1_DirectMethodCall(_myClass);

        [Benchmark]
        public void Way2() => _mi.Way2_CreateLambdaCall(_myClass);

        [Benchmark]
        public void Way3() => _mi.Way3_RelectionAPIMethodInvoke(_myClass);

        [Benchmark]
        public void Way4() => _mi.Way4_Using_dynamic_Keyword(_myClass);

        [Benchmark]
        public void Way5() => _mi.Way5_CreateDelegateCall(_myClass);

        [Benchmark]
        public void Way6() => _mi.Way6_CreateExpressionCall(_myClass);

        [Benchmark]
        public void Way7() => _mi.Way7_EmitAPIDynamicMethod(_myClass);
    }
}
