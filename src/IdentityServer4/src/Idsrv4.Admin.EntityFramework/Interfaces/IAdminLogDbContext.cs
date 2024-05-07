using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Entities;

namespace Idsrv4.Admin.EntityFramework.Interfaces;

public interface IAdminLogDbContext
{
    DbSet<Log> Logs { get; set; }
}