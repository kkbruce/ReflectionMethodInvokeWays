using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionMethodInvokeWays
{
    /// <summary>
    /// 受測類別
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// Foo 受測方法
        /// </summary>
        /// <param name="value">int 型別值</param>
        /// <returns></returns>
        public string Foo(int value)
        {
            return $"Foo value: {value}";
        }

        /// <summary>
        /// Bar 受測方法
        /// </summary>
        /// <param name="value">string 型別值</param>
        /// <returns></returns>
        public string Bar(string value)
        {
            return $"Bar value: {value}";
        }
    }
}
