using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReflectionMethodInvokeWays
{
    class Program
    {
        static void Main(string[] args)
        {
            object myClass = new MyClass();

            var mi = new MethodInvoke();
            mi.Way1_DirectMethodCall(myClass);
            mi.Way2_CreateALambdaCall(myClass);
            mi.Way3_UsingMethodInvoke(myClass);
            mi.Way4_Using_dynamic_Keyword(myClass);
            mi.Way5_CreateDelegateCall(myClass);
            mi.Way6_CreateExpresionCall(myClass);
            mi.Way7_UsingDynamicMethodCall(myClass);
            Console.Read();
        }
    }
}
