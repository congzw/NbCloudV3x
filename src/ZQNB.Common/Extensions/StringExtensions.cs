using System;
using System.Collections.Generic;
using System.Linq;

namespace ZQNB.Common.Extensions
{
    public static class StringExtensions
    {
        //扩展的字符串包含比较
        /// <summary>
        /// 比较是否包含，默认忽略大小写，忽略各种空差异
        /// </summary>
        /// <param name="list"></param>
        /// <param name="toCheck"></param>
        /// <param name="comparer"></param>
        /// <param name="trimSpaceBeforeCompare"></param>
        /// <returns></returns>
        public static bool NbContains(this IEnumerable<string> list, string toCheck, StringComparer comparer = null, bool trimSpaceBeforeCompare = true)
        {
            if (comparer == null)
            {
                comparer = StringComparer.OrdinalIgnoreCase;
            }

            string fixString = string.Empty;
            if (toCheck != null)
            {
                fixString = trimSpaceBeforeCompare ? toCheck.Trim() : toCheck;
            }

            return list.Contains(fixString, comparer);
        }

    }
}
