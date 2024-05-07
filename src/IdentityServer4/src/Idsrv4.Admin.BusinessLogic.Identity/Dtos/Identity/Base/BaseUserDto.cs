using System.Collections.Generic;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseUserDto<TUserId> : IBaseUserDto
{
    public TUserId Id { get; set; }

    public bool IsDefaultId() => EqualityComparer<TUserId>.Default.Equals(Id, default);

    object IBaseUserDto.Id => Id;
}