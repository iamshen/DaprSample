using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseUserChangePasswordDto<TUserId> : IBaseUserChangePasswordDto
{
    public TUserId UserId { get; set; }

    object IBaseUserChangePasswordDto.UserId => UserId;
}