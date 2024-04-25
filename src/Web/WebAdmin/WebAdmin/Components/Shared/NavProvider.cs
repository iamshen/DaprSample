using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;

namespace WebAdmin.Components.Shared;

public class NavProvider
{
    public NavProvider()
    {
        NavMenuItems =
        [
            new NavLink(
                href: "/",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.Home(),
                name: "Home"
            ),

            new NavLink(
                href: "/counter",
                match: NavLinkMatch.All,
                icon: new Icons.Regular.Size20.NumberSymbolSquare(),
                name: "Counter"
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
                        href: "/customer/list",
                        icon: new Icons.Regular.Size20.Accessibility(),
                        name: "CustomerList"
                    ),
                    new NavLink(
                        href: "/customer/level",
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
                        href: "/order/purchase",
                        icon: new Icons.Regular.Size20.ShoppingBag(),
                        name: "PurchaseOrder"
                    ),
                    new NavLink(
                        href: "/order/process",
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
                        href: "/payment/receiving-orders",
                        icon: new Icons.Regular.Size20.CreditCardClock(),
                        name: "ReceiveOrder"
                    ),
                    new NavLink(
                        href: "/payment/paid-orders",
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
                        href: "/logistics/orders",
                        icon: new Icons.Regular.Size20.BoxMultipleSearch(),
                        name: "LogisticsOrder"
                    ),
                    new NavLink(
                        href: "/logistics/services",
                        icon: new Icons.Regular.Size20.Globe(),
                        name: "LogisticsService"
                    ),
                    new NavLink(
                        href: "/logistics/channels",
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
                        href: "/device/list",
                        icon: new Icons.Regular.Size20.DeviceMeetingRoom(),
                        name: "DeviceList"
                    ),
                    new NavLink(
                        href: "/device/types",
                        icon: new Icons.Regular.Size20.AppsList(),
                        name: "DeviceType"
                    ),
                    new NavLink(
                        href: "/device/models",
                        icon: new Icons.Regular.Size20.Cube(),
                        name: "DeviceModule"
                    ),
                    new NavLink(
                        href: "/device/maintenance-orders",
                        icon: new Icons.Regular.Size20.VirtualNetworkToolbox(),
                        name: "DeviceMaintain"
                    ),
                    new NavLink(
                        href: "/device/remote-controls",
                        icon: new Icons.Regular.Size20.DesktopCursor(),
                        name: "RemoveControl"
                    ),
                    new NavLink(
                        href: "/device/versions",
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
}