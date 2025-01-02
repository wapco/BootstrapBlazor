using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// Loading组件
/// </summary>
public class Loading : ComponentBase
{
    private bool ShowLoading { get; set; }

    [Inject]
    private LoadingService LoadingService { get; set; } = default!;

    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("form-loader")
        .AddClass("show", ShowLoading)
        .AddClass("fade", !ShowLoading)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadingService.Register(this, Toggle);
    }

    /// <summary>
    /// Toggle 方法
    /// </summary>
    private Task Toggle(bool show)
    {
        ShowLoading = show;
        return InvokeAsync(StateHasChanged);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        int sequence = 0;
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", ClassName);
        builder.AddAttribute(sequence++, "style", "z-index: 2000");
        builder.OpenComponent<Spinner>(sequence++);
        builder.AddAttribute(sequence++, nameof(Spinner.Color), Color.Primary);
        builder.CloseComponent();
        builder.CloseElement();
    }
}
