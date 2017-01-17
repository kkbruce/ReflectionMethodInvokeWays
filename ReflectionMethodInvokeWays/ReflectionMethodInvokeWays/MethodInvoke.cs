using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionMethodInvokeWays
{
    internal class MethodInvoke
    {
        /// <summary>
        /// Way7： 使用 Reflection.Emit.DynamicMethod 動態產生 IL 並透過 CreateDelegate 建立 Func 委派執行方法。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way7_UsingDynamicMethodCall(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;
            var type = targetObject.GetType();

            var fooMethod = type.GetMethod("Foo");

            // Ref：https://msdn.microsoft.com/zh-tw/library/system.reflection.emit.dynamicmethod(v=vs.110).aspx
            var fooDynamicMethod = new DynamicMethod("Foo_",
                typeof(string),
                new[] { type, typeof(int) },
                true);
            var fooIl = fooDynamicMethod.GetILGenerator();
            fooIl.DeclareLocal(typeof(string));
            fooIl.Emit(OpCodes.Ldarg_0);
            fooIl.Emit(OpCodes.Ldarg_1);
            fooIl.Emit(OpCodes.Call, fooMethod);
            fooIl.Emit(OpCodes.Ret);

            var fooFunc = (Func<int, string>)fooDynamicMethod.CreateDelegate(typeof(Func<int, string>), targetObject);
            var fooResult = fooFunc(7);

            var barMethod = type.GetMethod("Bar");
            var barDynamicMethod = new DynamicMethod("Bar_",
                typeof(string),
                new[] { type, typeof(string) },
                true);
            var barIl = barDynamicMethod.GetILGenerator();
            barIl.DeclareLocal(typeof(string));
            barIl.Emit(OpCodes.Ldarg_0);
            barIl.Emit(OpCodes.Ldarg_1);
            barIl.Emit(OpCodes.Call, barMethod);
            barIl.Emit(OpCodes.Ret);

            var barFunc = (Func<string, string>)barDynamicMethod.CreateDelegate(typeof(Func<string, string>), targetObject);
            var barResult = barFunc("Nancy");

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way6： 建立 expresion 並進行方法呼叫。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way6_CreateExpressionCall(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;
            var thisObject = Expression.Constant(targetObject);

            var fooMethod = targetObject.GetType().GetMethod("Foo");
            var intValue = Expression.Parameter(typeof(int), "value");
            var fooCall = Expression.Call(thisObject, fooMethod, intValue);
            var fooLambda = Expression.Lambda<Func<int, string>>(fooCall, intValue);
            var fooFunc = fooLambda.Compile();
            var fooResult = fooFunc(6);

            var barMethod = targetObject.GetType().GetMethod("Bar");
            var strValue = Expression.Parameter(typeof(string), "value");
            var barCall = Expression.Call(thisObject, barMethod, strValue);
            var barLambda = Expression.Lambda<Func<string, string>>(barCall, strValue);
            var barFunc = barLambda.Compile();
            var barResult = barFunc("Metilda");

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way5：使用 Delegate.CreateDelegate 建立委派並呼叫方法。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way5_CreateDelegateCall(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;

            var fooMethod = targetObject.GetType().GetMethod("Foo");
            var fooFunc = (Func<int, string>)Delegate.CreateDelegate(typeof(Func<int, string>), targetObject, fooMethod);
            var fooResult = fooFunc(5);

            var barMethod = targetObject.GetType().GetMethod("Bar");
            var barFunc = (Func<string, string>)Delegate.CreateDelegate(typeof(Func<string, string>), targetObject, barMethod);
            var barResult = barFunc("Gina");

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way4：使用 dynamic 關鍵字。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way4_Using_dynamic_Keyword(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;

            // 缺點：失去強型別效果，但速度不差
            var dynamicTarget = (dynamic)targetObject;
            // .Foo 不會有 IntelliSense 提示。
            var fooResult = dynamicTarget.Foo(4) as string;
            // .Bar 不會有 IntelliSense 提示。
            var barResult = dynamicTarget.Bar("Love") as string;

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way3：使用純反射(反映)的 Methoid.Invoke 進行方法呼叫
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way3_UsingMethodInvoke(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;

            var foo = targetObject.GetType().GetMethod("Foo");
            // GetTypeInfo() 是比較新的 API，支援跨平台
            // var foo2 = targetObject.GetType().GetTypeInfo().GetDeclaredMethod("Foo");
            var fooResult = foo.Invoke(targetObject, new object[] { 3 }) as string;

            var bar = targetObject.GetType().GetMethod("Bar");
            var barResult = bar.Invoke(targetObject, new object[] { "Happy" }) as string;

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way2：建立 lambda 並進行方法呼叫。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way2_CreateALambdaCall(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;

            Func<int, string> fooFunc = f => ((MyClass)targetObject).Foo(f);
            var fooResult = fooFunc(2);

            Func<string, string> barFunc = b => ((MyClass)targetObject).Bar(b);
            var barResult = barFunc("Sherry");

            PrintResult(baseName, fooResult, barResult);
        }

        /// <summary>
        /// Way1：物件直接進行方法呼叫。
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        internal void Way1_DirectMethodCall(object targetObject)
        {
            var baseName = MethodBase.GetCurrentMethod().Name;

            var myClass = ((MyClass)targetObject);
            var fooResult = myClass.Foo(1);
            var barResult = myClass.Bar("Bruce");

            PrintResult(baseName, fooResult, barResult);
        }

        [Conditional("DEBUG")]
        private void PrintResult(string baseName, string foo, string bar)
        {
            Console.WriteLine($"{baseName}: {foo}, {bar}");
        }
    }
}