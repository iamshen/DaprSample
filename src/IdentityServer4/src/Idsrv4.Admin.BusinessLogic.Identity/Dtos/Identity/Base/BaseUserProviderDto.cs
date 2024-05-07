using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseUserProviderDto<TUserId> : IBaseUserProviderDto
{
    public TUserId UserId { get; set; }

    object IBaseUserProviderDto.UserId => UserId;
}