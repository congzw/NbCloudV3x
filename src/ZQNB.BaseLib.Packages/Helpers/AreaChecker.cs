using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZQNB.Common;
using ZQNB.Common.Extensions;

namespace ZQNB.BaseLib.Packages.Helpers
{
    /// <summary>
    /// 模块检查
    /// </summary>
    public class AreaChecker
    {
        /// <summary>
        /// 某个模块是否存在于WebBin下
        /// </summary>
        /// <param name="area"></param>
        /// <param name="binPath"></param>
        /// <returns></returns>
        public static bool AreaExistInWebBin(string area, string binPath = null)
        {
            var findAreasInWebBin = FindAreasInWebBin(binPath);
            return findAreasInWebBin.NbContains(area);
        }

        /// <summary>
        /// WebBin下的所有模块
        /// </summary>
        /// <returns></returns>
        public static IList<string> FindAreasInWebBin(string binPath = null)
        {
            var binPathFix = binPath;
            if (binPathFix == null)
            {
                binPathFix = GetWebBinDirPath();
            }

            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;
            string patterns = string.Format("{0}.Web.Areas.*.dll", defaultProjectPrefix);
            var assemblies = FindAssemlbiesInBinDirPath(binPathFix, patterns, SearchOption.TopDirectoryOnly, null).ToList();
            //OK: FTC.dll
            //KO: FTC.Lib.dll
            //KO: FTCLib.dll
            var areaAassemblies = assemblies.Where(x => !x.EndsWith("Lib.dll", StringComparison.OrdinalIgnoreCase)).ToList();
            
            var areas = new List<string>();
            foreach (var areaAassembly in areaAassemblies)
            {
                //ZQNB.Web.Areas.*.dll
                //{x}.[{0}.{1}.{2}.dll]"
                var indexOf = areaAassembly.IndexOf("Web.Areas.", StringComparison.OrdinalIgnoreCase);
                var substring = areaAassembly.Substring(indexOf);
                var strings = substring.Split('.');
                areas.Add(strings[2]);
            }
            return areas;
        }
        
        /// <summary>
        /// WebBin下的某个模块Lib.dll
        /// </summary>
        /// <returns></returns>
        public static string TryGetAreaLibDllPathInWebBin(string area, string binPath = null)
        {
            var binPathFix = binPath;
            if (binPathFix == null)
            {
                binPathFix = GetWebBinDirPath();
            }

            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;
            string patterns = string.Format("{0}.Web.Areas.{1}.Lib.dll", defaultProjectPrefix, area);
            var assemblies = FindAssemlbiesInBinDirPath(binPathFix, patterns, SearchOption.TopDirectoryOnly, null).ToList();
            if (assemblies.Count == 0)
            {
                patterns = string.Format("{0}.Web.Areas.{1}Lib.dll", defaultProjectPrefix, area);
                assemblies = FindAssemlbiesInBinDirPath(binPathFix, patterns, SearchOption.TopDirectoryOnly, null).ToList();
            }

            if (patterns.Length == 0)
            {
                return null;
            }

            return assemblies.SingleOrDefault();
        }

        /// <summary>
        /// WebBin下的某个模块Dll
        /// </summary>
        /// <returns></returns>
        public static string TryGetAreaDllPathInWebBin(string area, string binPath = null)
        {
            var binPathFix = binPath;
            if (binPathFix == null)
            {
                binPathFix = GetWebBinDirPath();
            }

            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;
            string patterns = string.Format("{0}.Web.Areas.{1}.dll", defaultProjectPrefix, area);
            var assemblies = FindAssemlbiesInBinDirPath(binPathFix, patterns, SearchOption.TopDirectoryOnly, null).ToList();

            if (patterns.Length == 0)
            {
                return null;
            }

            return assemblies.SingleOrDefault();
        }

        /// <summary>
        /// 查找WebBin的路径
        /// </summary>
        /// <returns></returns>
        public static string GetWebBinDirPath()
        {
            string binPath = MyPathHelper.MakeWebBinDirPath();
            return binPath;
        }

        private static List<string> FindAssemlbiesInBinDirPath(string binPath, string match, SearchOption searchOption = SearchOption.TopDirectoryOnly, string[] excludeFiles = null)
        {
            if (string.IsNullOrWhiteSpace(match))
            {
                throw new ArgumentNullException("match");
            }

            var files = MyIOHelper.GetFiles(binPath, match, searchOption);
            if (excludeFiles == null)
            {
                return files;
            }
            var filesFix = files.Where(file => !excludeFiles.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToList();
            return filesFix;
        }
    }
}