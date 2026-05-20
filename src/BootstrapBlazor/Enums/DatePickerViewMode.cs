// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTimePicker 组件视图显示模式</para>
/// <para lang="en">DateTimePicker Components View Mode</para>
/// </summary>
public enum DatePickerViewMode
{
    /// <summary>
    /// <para lang="zh">年月日时分秒模式</para>
    /// <para lang="en">DateTime Mode</para>
    /// </summary>
    [Description("yyyy-MM-dd HH:mm:ss")]
    DateTime,

    /// <summary>
    /// <para lang="zh">年月日时分模式</para>
    /// <para lang="en">Date Minute Mode</para>
    /// </summary>
    [Description("yyyy-MM-dd HH:mm")]
    DateMinute,

    /// <summary>
    /// <para lang="zh">年月日模式</para>
    /// <para lang="en">Date Mode</para>
    /// </summary>
    [Description("yyyy-MM-dd")]
    Date,

    /// <summary>
    /// <para lang="zh">月视图</para>
    /// <para lang="en">Month Mode</para>
    /// </summary>
    [Description("yyyy-MM")]
    Month,

    /// <summary>
    /// <para lang="zh">年视图</para>
    /// <para lang="en">Year Mode</para>
    /// </summary>
    [Description("yyyy")]
    Year
}
