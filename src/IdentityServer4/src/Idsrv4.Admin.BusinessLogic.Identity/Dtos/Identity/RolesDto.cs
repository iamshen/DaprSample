using System.Collections.Generic;
using System.Linq;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity;

public class RolesDto<TRoleDto, TKey> : IRolesDto where TRoleDto : RoleDto<TKey>
{
    public RolesDto()
    {
        Roles = new List<TRoleDto>();
    }

    public List<TRoleDto> Roles { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    List<IRoleDto> IRolesDto.Roles => Roles.Cast<IRoleDto>().ToList();
}