using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using WebAdmin.Shared.Records;
using NavLink = WebAdmin.Shared.Records.NavLink;

namespace WebAdmin.Shared.Infrastructure;

public class NavProvider
{
    public NavProvider()
    {
        NavMenuItems =
        [
            new NavLink(
                href: "/admin/",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.Home(),
                name: "Home"
            ),

            new NavLink(
                href: "counter",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.NumberSymbolSquare(),
                name: "Counter"
            ),


            new NavLink(
                href: "weather",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.PersonKey(),
                name: "Weather"
            ),

            new NavLink(
                href: "userProfile",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.PersonBoard(),
                name: "UserProfile",
                isHidden: true
            ),

            // 会员管理
            new NavGroup(
                icon: new Icons.Regular.Size20.Group(),
                name: "CustomerMgr",
                expanded: true,
                gap: "10px",
                children:
                [
                    new NavLink(
                        href: "customer/list",
                        icon: new Icons.Regular.Size20.Accessibility(),
                        name: "CustomerList"
                    ),
                    new NavLink(
                        href: "customer/level",
                        icon: new Icons.Regular.Size20.Trophy(),
                        name: "CustomerLevel"
                    )
                ]),

            // 订单管理
            new NavGroup(
                icon: new Icons.Regular.Size20.ReceiptCube(),
                name: "OrderMgr",
                expanded: false,
                gap: "10px",
                children:
                [
                    new NavLink(
                        href: "order/purchase",
                        icon: new Icons.Regular.Size20.ShoppingBag(),
                        name: "PurchaseOrder"
                    ),
                    new NavLink(
                        href: "order/process",
                        icon: new Icons.Regular.Size20.BuildingFactory(),
                        name: "ProcessOrder"
                    )
                ]),

            // 支付管理
            new NavGroup(
                icon: new Icons.Regular.Size20.CurrencyDollarEuro(),
                name: "PaymentMgr",
                expanded: false,
                gap: "10px",
                children:
                [
                    new NavLink(
                        href: "payment/receiving-orders",
                        icon: new Icons.Regular.Size20.CreditCardClock(),
                        name: "ReceiveOrder"
                    ),
                    new NavLink(
                        href: "payment/paid-orders",
                        icon: new Icons.Regular.Size20.WalletCreditCard(),
                        name: "PaidOrder"
                    )
                ]),

            // 物流管理
            new NavGroup(
                icon: new Icons.Regular.Size20.VehicleTruckBag(),
                name: "LogisticsMgr",
                expanded: false,
                gap: "10px",
                children:
                [
                    new NavLink(
                        href: "logistics/orders",
                        icon: new Icons.Regular.Size20.BoxMultipleSearch(),
                        name: "LogisticsOrder"
                    ),
                    new NavLink(
                        href: "logistics/services",
                        icon: new Icons.Regular.Size20.Globe(),
                        name: "LogisticsService"
                    ),
                    new NavLink(
                        href: "logistics/channels",
                        icon: new Icons.Regular.Size20.CompassNorthwest(),
                        name: "LogisticsChannel"
                    )
                ]),

            // 终端管理
            new NavGroup(
                icon: new Icons.Regular.Size20.TabletLaptop(),
                name: "DeviceMgr",
                expanded: false,
                gap: "10px",
                children:
                [
                    new NavLink(
                        href: "device/list",
                        icon: new Icons.Regular.Size20.DeviceMeetingRoom(),
                        name: "DeviceList"
                    ),
                    new NavLink(
                        href: "device/types",
                        icon: new Icons.Regular.Size20.AppsList(),
                        name: "DeviceType"
                    ),
                    new NavLink(
                        href: "device/models",
                        icon: new Icons.Regular.Size20.Cube(),
                        name: "DeviceModule"
                    ),
                    new NavLink(
                        href: "device/maintenance-orders",
                        icon: new Icons.Regular.Size20.VirtualNetworkToolbox(),
                        name: "DeviceMaintain"
                    ),
                    new NavLink(
                        href: "device/remote-controls",
                        icon: new Icons.Regular.Size20.DesktopCursor(),
                        name: "RemoveControl"
                    ),
                    new NavLink(
                        href: "device/versions",
                        icon: new Icons.Regular.Size20.BranchFork(),
                        name: "VersionMgr"
                    )
                ]),
        ];

        FlattenedMenuItems = GetFlattenedMenuItems(NavMenuItems)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<NavItem> NavMenuItems { get; init; }

    public IReadOnlyList<NavItem> FlattenedMenuItems { get; init; }
    private static IEnumerable<NavItem> GetFlattenedMenuItems(IEnumerable<NavItem> items)
    {
        foreach (var item in items)
        {
            yield return item;

            if (item is not NavGroup group || !group.Children.Any()) continue;

            foreach (var flattenedMenuItem in GetFlattenedMenuItems(group.Children)) yield return flattenedMenuItem;
        }
    }

    public List<NavItem> FindNavItemAndBuildBreadcrumb(string searchName, IReadOnlyList<NavItem> items, List<NavItem>? breadcrumb = null)
    {
        breadcrumb ??= [];

        foreach (var item in items)
        {
            // 如果找到匹配的项，返回包含当前项的面包屑链
            if (item.Name == searchName)
            {
                breadcrumb.Add(item);
                return breadcrumb;
            }

            // 如果是 NavGroup，递归搜索其子项
            if (item is NavGroup group)
            {
                breadcrumb.Add(item);  // 将当前 NavGroup 添加到面包屑中
                var result = FindNavItemAndBuildBreadcrumb(searchName, group.Children, breadcrumb);
                if (result != null) return result;  // 如果在子项中找到了，返回结果
                breadcrumb.RemoveAt(breadcrumb.Count - 1);  // 如果没有找到，从面包屑中移除当前 NavGroup
            }
        }

        return [];  // 如果遍历完成后未找到任何匹配项，返回 []
    }

    public NavItem? FindNavItemByHref(string href, IReadOnlyList<NavItem> items)
    {
        foreach (var item in items)
        {
            // 检查当前项的 href 是否匹配
            if (item.Href == href)
            {
                return item;
            }

            // 如果是 NavGroup，递归搜索其子项
            if (item is NavGroup group)
            {
                var foundItem = FindNavItemByHref(href, group.Children);
                if (foundItem != null)
                {
                    return foundItem;
                }
            }
        }

        return null;  // 如果遍历完成后未找到任何匹配项，返回 null
    }

}