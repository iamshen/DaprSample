using Idsrv4.Admin.BusinessLogic.Helpers;

namespace Idsrv4.Admin.BusinessLogic.Resources;

public interface IPersistedGrantServiceResources
{
    ResourceMessage PersistedGrantDoesNotExist();

    ResourceMessage PersistedGrantWithSubjectIdDoesNotExist();
}