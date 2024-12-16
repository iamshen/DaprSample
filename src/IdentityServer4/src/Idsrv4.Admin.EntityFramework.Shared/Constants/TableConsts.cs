namespace Idsrv4.Admin.EntityFramework.Shared.Constants;

public static class TableConsts
{
    public const string Schema = "public";
    public const string TablePrefix = "ids_";

    // Identity
    public const string IdentityRoles = TablePrefix + "Roles";
    public const string IdentityRoleClaims = TablePrefix + "RoleClaims";
    public const string IdentityUserRoles = TablePrefix + "UserRoles";
    public const string IdentityUsers = TablePrefix + "Users";
    public const string IdentityUserLogins = TablePrefix + "UserLogins";
    public const string IdentityUserClaims = TablePrefix + "UserClaims";
    public const string IdentityUserTokens = TablePrefix + "UserTokens";
    // Configuration
    public static string IdentityResource = TablePrefix + "IdentityResource";
    public static string IdentityResourceProperty = TablePrefix + "IdentityResourceProperty";
    public static string IdentityClaim = TablePrefix + "IdentityClaim";
    public static string ApiSecret = TablePrefix + "ApiSecret";
    public static string ApiScope = TablePrefix + "ApiScope";
    public static string ApiScopeClaim = TablePrefix + "ApiScopeClaim";
    public static string ApiScopeProperty = TablePrefix + "ApiScopeProperty";
    public static string ApiResource = TablePrefix + "ApiResource";
    public static string ApiResourceProperty = TablePrefix + "ApiResourceProperty";
    public static string ApiResourceClaim = TablePrefix + "ApiResourceClaim";
    public static string ApiResourceScope = TablePrefix + "ApiResourceScope";
    public static string ClientGrantType = TablePrefix + "ClientGrantType";
    public static string ClientScope = TablePrefix + "ClientScope";
    public static string ClientSecret = TablePrefix + "ClientSecret";
    public static string ClientPostLogoutRedirectUri = TablePrefix + "ClientPostLogoutRedirectUri";
    public static string ClientCorsOrigin = TablePrefix + "ClientCorsOrigin";
    public static string ClientIdPRestriction = TablePrefix + "ClientIdPRestriction";
    public static string ClientRedirectUri = TablePrefix + "ClientRedirectUri";
    public static string ClientClaim = TablePrefix + "ClientClaim";
    public static string ClientProperty = TablePrefix + "ClientProperty";
    public static string Client = TablePrefix + "Client";
    // PersistedGrant
    public static string PersistedGrant = TablePrefix + "PersistedGrant";
    public static string DeviceFlowCodes = TablePrefix + "DeviceFlowCodes";
    // Log
    public const string AuditLog = TablePrefix + "AuditLog";  
    public const string Logging = TablePrefix + "Log";

    // DataProtect
    public static string DataProtectionKey = TablePrefix + "DataProtectionKey";
}