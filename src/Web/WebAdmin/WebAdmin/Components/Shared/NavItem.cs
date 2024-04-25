using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;

namespace WebAdmin.Components.Shared;

public abstract record NavItem
{
    public string Name { get; init; } = string.Empty;
    public string? Href { get; init; }
    public NavLinkMatch Match { get; init; } = NavLinkMatch.Prefix;
    public Icon Icon { get; init; } = new Icons.Regular.Size20.Document();
    public Color IconColor { get; set; } = Color.Accent;
}

public record NavLink : NavItem
{
    public NavLink(
        string? href, 
        Icon icon, 
        string name, 
        NavLinkMatch match = NavLinkMatch.Prefix,
        Color iconColor = Color.Accent)
    {
        Href = href;
        Icon = icon;
        Name = name;
        Match = match;
        IconColor = iconColor;
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