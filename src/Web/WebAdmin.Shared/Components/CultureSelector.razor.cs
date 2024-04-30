using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using WebAdmin.Shared.Configurations;

namespace WebAdmin.Shared.Components;

public partial class CultureSelector
{
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject] protected CultureConfiguration CultureConfiguration { get; set; } = null!;

    private string _culture = string.Empty;

    public string Culture
    {
        get => _culture;
        set
        {
            _culture = value;
            ChangeCulture(value);
        }
    }

    private static readonly List<Option<string>> CultureOptions =
    [
        new Option<string> { Value = "zh-Hans", Text = "中文（简体）" },
        new Option<string> { Value = "en", Text = "English" }
    ];


    protected override Task OnInitializedAsync()
    {
        _culture = CultureInfo.CurrentCulture.Name;

        return base.OnInitializedAsync();
    }

    public void ChangeCulture(string? _)
    {
        var redirect = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery | UriComponents.Fragment,
            UriFormat.UriEscaped);

        var query = $"?culture={Uri.EscapeDataString(_culture)}&redirectUri={redirect}";

        NavigationManager.NavigateTo("Culture/SetCulture" + query, true);
    }
}