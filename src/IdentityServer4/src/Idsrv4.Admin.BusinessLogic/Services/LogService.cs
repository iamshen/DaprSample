﻿using System;
using System.Threading.Tasks;
using Idsrv4.Admin.BusinessLogic.Dtos.Log;
using Idsrv4.Admin.BusinessLogic.Events.Log;
using Idsrv4.Admin.BusinessLogic.Mappers;
using Idsrv4.Admin.BusinessLogic.Services.Interfaces;
using Idsrv4.Admin.EntityFramework.Repositories.Interfaces;
using Idsrv4.Admin.AuditLogging.Services;

namespace Idsrv4.Admin.BusinessLogic.Services;

public class LogService : ILogService
{
    protected readonly IAuditEventLogger AuditEventLogger;
    protected readonly ILogRepository Repository;

    public LogService(ILogRepository repository, IAuditEventLogger auditEventLogger)
    {
        Repository = repository;
        AuditEventLogger = auditEventLogger;
    }

    public virtual async Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10)
    {
        var pagedList = await Repository.GetLogsAsync(search, page, pageSize);
        var logs = pagedList.ToModel();

        await AuditEventLogger.LogEventAsync(new LogsRequestedEvent());

        return logs;
    }

    public virtual async Task DeleteLogsOlderThanAsync(DateTime deleteOlderThan)
    {
        await Repository.DeleteLogsOlderThanAsync(deleteOlderThan);

        await AuditEventLogger.LogEventAsync(new LogsDeletedEvent(deleteOlderThan));
    }
}