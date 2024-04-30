using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using Microsoft.FluentUI.AspNetCore.Components;
using WebAdmin.Shared.Infrastructure;

namespace WebAdmin.Shared.Components;

public partial class SiteSettingsPanel
{
    private string? _status;
    private bool _popVisible;
    private bool _ltr = true;
    private FluentDesignTheme? _theme;

    [Inject]
    public required ILogger<SiteSettingsPanel> Logger { get; set; }

    [Inject]
    public required CacheStorageAccessor CacheStorageAccessor { get; set; }

    [Inject]
    public required GlobalState GlobalState { get; set; }

    public DesignThemeModes Mode { get; set; }

    public OfficeColor? OfficeColor { get; set; }

    public LocalizationDirection? Direction { get; set; }

    private static IEnumerable<DesignThemeModes> AllModes => Enum.GetValues<DesignThemeModes>();

    private static IEnumerable<OfficeColor?> AllOfficeColors
    {
        get
        {
            return Enum.GetValues<OfficeColor>().Select(i => (OfficeColor?)i);
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Direction = GlobalState.Dir;
            _ltr = !Direction.HasValue || Direction.Value == LocalizationDirection.LeftToRight;
        }
    }

    protected void HandleDirectionChanged(bool isLeftToRight)
    {

        _ltr = isLeftToRight;
        Direction = isLeftToRight ? LocalizationDirection.LeftToRight : LocalizationDirection.RightToLeft;
    }

    private async Task ResetSiteAsync()
    {
        var msg = "重置网站设置并清除缓存！";

        await CacheStorageAccessor.RemoveAllAsync();
        _theme?.ClearLocalStorageAsync();

        Logger.LogInformation(msg);
        _status = msg;

        OfficeColor = Microsoft.FluentUI.AspNetCore.Components.OfficeColor.Default;
        Mode = DesignThemeModes.System;
    }

    private async Task RandomSiteAsync()
    {
        var msg = "随机网站主题颜色和模式！";

        await CacheStorageAccessor.RemoveAllAsync();
        _theme?.ClearLocalStorageAsync();

        Logger.LogInformation(msg);
        _status = msg;

        OfficeColor = OfficeColorUtilities.GetRandom();
        var themeModes = Enum.GetValues<DesignThemeModes>();
        Mode = themeModes.ElementAt(Random.Shared.Next(themeModes.Length));
    }

    private static string? GetCustomColor(OfficeColor? color)
    {
        return color switch
        {
            null => OfficeColorUtilities.GetRandom(true).ToAttributeValue(),
            Microsoft.FluentUI.AspNetCore.Components.OfficeColor.Default => "#036ac4",
            _ => color.ToAttributeValue(),
        };

    }
}