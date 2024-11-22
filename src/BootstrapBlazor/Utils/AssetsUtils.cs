using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 静态资源文件工具类
/// </summary>
public static class AssetsUtils
{
    private static string? _version;

    public static string AssetsVersion
    {
        get
        {
            if (string.IsNullOrEmpty(_version))
            {
#if DEBUG
                return DateTime.Now.ToString("yyyyMMddHHmmss");
#endif
                try
                {
                    // 获取当前程序集
                    var assembly = Assembly.GetExecutingAssembly();
                    // 获取程序集的版本号
                    var version = assembly.GetName().Version;
                    _version = version?.ToString() ?? "1.0";
                }
                catch
                {
                    _version = "_";
                }
            }

            return _version;
        }
    }

    public static void SetCdnPath(string? cdnPath)
    {
        if (cdnPath == null)
        {
            throw new ArgumentException("未配置CDN路径");
        }

        _cdnPath = cdnPath.TrimEnd('/') + "/";
    }

    private static string? _cdnPath = null;

    public static string Path(string path, bool version = true)
    {
        return version ? $"{_cdnPath}{path}?v={AssetsVersion}" : $"{_cdnPath}{path}";
    }
}
