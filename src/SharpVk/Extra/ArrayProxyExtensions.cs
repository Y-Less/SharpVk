using System;
using System.Collections.Generic;
using System.Text;

namespace SharpVk.Extra
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayProxyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy"></param>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static ArrayProxy<T> AddUnique<T>(this ArrayProxy<T> proxy, T elem)
        {
            int len = proxy.Length;
            var ret = new T[len + 1];
            bool found = false;
            for (int i = 0; i != len; ++i)
            {
                ret[i] = proxy[i];
                found = found || proxy[i].Equals(elem);
            }
            if (found)
            {
                return proxy;
            }
            else
            {
                ret[len] = elem;
                return ret;
            }
        }
    }
}
