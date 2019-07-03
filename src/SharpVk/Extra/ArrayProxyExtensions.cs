using System;
using System.Collections.Generic;
using System.Text;

namespace SharpVk.Extra
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ArrayProxyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy"></param>
        /// <param name="elem"></param>
        /// <returns></returns>
        internal static ArrayProxy<T> AddUnique<T>(this ArrayProxy<T> proxy, T elem)
        {
            int len = proxy.Length;
            for (int i = 0; i != len; ++i)
            {
                if (proxy[i].Equals(elem))
                {
                    return proxy;
                }
            }
            var ret = new T[len + 1];
            for (int i = 0; i != len; ++i)
            {
                ret[i] = proxy[i];
            }
            ret[len] = elem;
            return ret;
        }
    }
}
