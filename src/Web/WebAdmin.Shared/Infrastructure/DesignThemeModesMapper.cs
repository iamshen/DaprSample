using Microsoft.FluentUI.AspNetCore.Components;

namespace WebAdmin.Shared.Infrastructure;

/// <summary />
public static class DesignThemeModesMapper
{
    /// <summary />
    public static string Map(DesignThemeModes? mode) => mode switch
    {
        DesignThemeModes.System => "跟随系统",
        DesignThemeModes.Light => "浅色",
        DesignThemeModes.Dark => "深色",
        null => "跟随系统",
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
    };
}