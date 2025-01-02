namespace BootstrapBlazor.Components;

/// <summary>
/// Loading服务
/// </summary>
public class LoadingService : BootstrapServiceBase<bool>
{
    /// <summary>
    /// 显示Loading方法
    /// </summary>
    public Task ShowLoading() => Invoke(true);

    /// <summary>
    /// 隐藏Loading方法
    /// </summary>
    /// <returns></returns>
    public Task CloseLoading() => Invoke(false);
}
