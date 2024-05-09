// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by 

using System;

namespace Idsrv4.Admin.STS.Identity.Configuration;

public class AccountOptions
{
    public static bool AllowLocalLogin = true;
    public static bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

    public static bool ShowLogoutPrompt = false;
    public static bool AutomaticRedirectAfterSignOut = true;

    public static string InvalidCredentialsErrorMessage = "Invalid username or password";
}