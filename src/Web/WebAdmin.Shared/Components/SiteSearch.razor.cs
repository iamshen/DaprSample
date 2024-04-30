using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Diagnostics;
using WebAdmin.Shared.Infrastructure;
using WebAdmin.Shared.Records;

namespace WebAdmin.Shared.Components;

public partial class SiteSearch
{
    [Inject]
    protected NavProvider NavProvider { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    private string? _searchTerm = "";
    private IEnumerable<NavItem>? _selectedOptions = [];
    private IReadOnlyList<NavItem> FlattenedMenuItems => NavProvider.FlattenedMenuItems
        .Select(x => new NavLink(href: x.Href, icon: x.Icon, match: x.Match, name: L[x.Name]))
        .ToList()
        .AsReadOnly();

    private void HandleSearchInput(OptionsSearchEventArgs<NavItem> e)
    {
        var searchTerm = e.Text;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            e.Items = FlattenedMenuItems;
        }
        else
        {
            e.Items = FlattenedMenuItems
                .Where(x => x.Href != null)
                .Where(x => x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }

    private void HandleSearchClicked()
    {
        _searchTerm = null;
        var targetHref = _selectedOptions?.SingleOrDefault()?.Href;
        _selectedOptions = [];
        InvokeAsync(StateHasChanged);

        // Ignore clearing the search bar
        if (targetHref is null)
        {
            return;
        }

        NavigationManager.NavigateTo(targetHref ?? throw new UnreachableException("无效项"));
    }
}