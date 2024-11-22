// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// CheckboxList、RadioList 自定义选项验证器
/// </summary>
public class CustomOptionValidator : RequiredValidator
{
    /// <summary>
    /// 获得/设置 IStringLocalizerFactory 注入服务实例 默认为 null
    /// </summary>
    public IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <inheritdoc/>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (propertyValue == null)
        {
            return;
        }

        if (propertyValue is string value && value.Contains("\"\""))
        {
            var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
            var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
            results.Add(new ValidationResult(errorMessage, memberNames));
        }
    }
}
