using Idsrv4.Admin.BusinessLogic.Identity.Helpers;

namespace Idsrv4.Admin.BusinessLogic.Identity.Resources;

public interface IPersistedGrantAspNetIdentityServiceResources
{
    ResourceMessage PersistedGrantDoesNotExist();

    ResourceMessage PersistedGrantWithSubjectIdDoesNotExist();
}