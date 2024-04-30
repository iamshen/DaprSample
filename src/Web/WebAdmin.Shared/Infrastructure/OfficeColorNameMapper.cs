using Microsoft.FluentUI.AspNetCore.Components;

namespace WebAdmin.Shared.Infrastructure;

/// <summary />
public static class OfficeColorNameMapper
{
    /// <summary />
    public static string Map(OfficeColor? color) => color switch
    {
        OfficeColor.Default => "源远流长",
        OfficeColor.Access => "朱砂古韵",
        OfficeColor.Booking => "碧波荡漾",
        OfficeColor.Exchange => "晴空万里",
        OfficeColor.Excel => "竹翠绿意",
        OfficeColor.GroupMe => "青天碧海",
        OfficeColor.Office => "朱门绯影",
        OfficeColor.OneDrive => "浩瀚蓝天",
        OfficeColor.OneNote => "紫气东来",
        OfficeColor.Outlook => "高望碧空",
        OfficeColor.Planner => "绿野策划",
        OfficeColor.PowerApps => "暗紫深思",
        OfficeColor.PowerBI => "金黄智慧",
        OfficeColor.PowerPoint => "赤日炎炎",
        OfficeColor.Project => "绿意盎然",
        OfficeColor.Publisher => "碧泉出版",
        OfficeColor.SharePoint => "分享蓝图",
        OfficeColor.Skype => "蓝天通话",
        OfficeColor.Stream => "红流媒体",
        OfficeColor.Sway => "水绿摇曳",
        OfficeColor.Teams => "深蓝合作",
        OfficeColor.Visio => "视觉蓝图",
        OfficeColor.Windows => "开窗见蓝",
        OfficeColor.Word => "字海泛蓝",
        OfficeColor.Yammer => "沟通蓝天",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
}