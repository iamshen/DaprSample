using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;

namespace WebAdmin.Shared.Records;

public abstract record NavItem
{
    public string Name { get; init; } = string.Empty;
    public string? Href { get; init; }
    public NavLinkMatch Match { get; init; } = NavLinkMatch.Prefix;
    public Icon Icon { get; init; } = new Icons.Regular.Size20.Document();
    public Color IconColor { get; set; } = Color.Accent;
    /// <summary> is hidden is the site navbar </summary>
    public bool IsHidden { get; set; } = false;
}

public record NavLink : NavItem
{
    public NavLink(
        string? href,
        Icon icon,
        string name,
        NavLinkMatch match = NavLinkMatch.Prefix,
        Color iconColor = Color.Accent,
        bool isHidden = false)
    {
        Href = href;
        Icon = icon;
        Name = name;
        Match = match;
        IconColor = iconColor;
        IsHidden = isHidden;
    }
}

public record NavGroup : NavItem
{
    public bool Expanded { get; init; }
    public string Gap { get; init; }
    public IReadOnlyList<NavItem> Children { get; }

    public NavGroup(Icon icon, string name, bool expanded, string gap, List<NavItem> children)
    {
        Href = null;
        Icon = icon;
        Name = name;
        Expanded = expanded;
        Gap = gap;
        Children = children.AsReadOnly();
    }
}