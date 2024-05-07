using System.Collections.Generic;
using Idsrv4.Admin.BusinessLogic.Shared.Dtos.Common;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

public interface IUserRolesDto : IBaseUserRolesDto
{
    string UserName { get; set; }
    List<SelectItemDto> RolesList { get; set; }
    List<IRoleDto> Roles { get; }
    int PageSize { get; set; }
    int TotalCount { get; set; }
}