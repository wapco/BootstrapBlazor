// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePicker 组件视图显示模式
/// </summary>
public enum DatePickerViewMode
{
    /// <summary>
    /// 年月日时分秒模式
    /// </summary>
    [Description("yyyy-MM-dd HH:mm:ss")]
    DateTime,

    /// <summary>
    /// 年月日时分模式
    /// </summary>
    [Description("yyyy-MM-dd HH:mm")]
    DateMinute,

    /// <summary>
    /// 年月日模式
    /// </summary>
    [Description("yyyy-MM-dd")]
    Date,

    /// <summary>
    /// 月视图
    /// </summary>
    [Description("yyyy-MM")]
    Month,

    /// <summary>
    /// 年视图
    /// </summary>
    [Description("yyyy")]
    Year
}
