using System.ComponentModel.DataAnnotations;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity;

public class RoleDto<TKey> : BaseRoleDto<TKey>, IRoleDto
{
    [Required] public string Name { get; set; }
}