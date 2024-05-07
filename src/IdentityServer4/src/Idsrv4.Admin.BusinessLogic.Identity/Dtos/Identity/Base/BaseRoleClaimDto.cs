using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseRoleClaimDto<TRoleId> : IBaseRoleClaimDto
{
    public TRoleId RoleId { get; set; }
    public int ClaimId { get; set; }

    object IBaseRoleClaimDto.RoleId => RoleId;
}