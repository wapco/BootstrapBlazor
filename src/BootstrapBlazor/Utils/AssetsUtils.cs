namespace BootstrapBlazor.Components;

/// <summary>
/// 静态资源文件工具类
/// </summary>
public static class AssetsUtils
{
    public static void SetCdnPath(string? cdnPath)
    {
        if (cdnPath == null)
        {
            throw new ArgumentException("未配置CDN路径");
        }

        _cdnPath = cdnPath.TrimEnd('/') + "/";
    }

    private static string? _cdnPath = null;

    public static string Path(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (string.IsNullOrEmpty(_cdnPath))
        {
            return path;
        }

        if (Uri.TryCreate(path, UriKind.Absolute, out _))
        {
            return path;
        }

        return $"{_cdnPath}{path.TrimStart('/')}";
    }
}
