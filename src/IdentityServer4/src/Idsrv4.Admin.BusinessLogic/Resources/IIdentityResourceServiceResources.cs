using Idsrv4.Admin.BusinessLogic.Helpers;

namespace Idsrv4.Admin.BusinessLogic.Resources;

public interface IIdentityResourceServiceResources
{
    ResourceMessage IdentityResourceDoesNotExist();

    ResourceMessage IdentityResourceExistsKey();

    ResourceMessage IdentityResourceExistsValue();

    ResourceMessage IdentityResourcePropertyDoesNotExist();

    ResourceMessage IdentityResourcePropertyExistsValue();

    ResourceMessage IdentityResourcePropertyExistsKey();
}