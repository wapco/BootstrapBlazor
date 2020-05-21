﻿using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Calendars
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private DateTime BindValue { get; set; } = DateTime.Today;

        private void OnValueChanged(DateTime ts)
        {
            Trace?.Log($"{ts:yyyy-MM-dd}");
        }

        private string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ValueChanged",
                Description="值改变时回调委托",
                Type ="EventCallback<DateTime>"
            },
            new EventItem()
            {
                Name = "OnValueChanged",
                Description="值改变时回调委托",
                Type ="Action<DateTime>"
            },
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Value",
                Description = "组件值",
                Type = "DateTime",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
