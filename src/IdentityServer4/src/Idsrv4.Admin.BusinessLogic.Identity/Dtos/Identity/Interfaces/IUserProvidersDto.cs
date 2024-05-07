using System.Collections.Generic;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

public interface IUserProvidersDto : IUserProviderDto
{
    List<IUserProviderDto> Providers { get; }
}