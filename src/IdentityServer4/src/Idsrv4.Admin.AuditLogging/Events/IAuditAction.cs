namespace Idsrv4.Admin.AuditLogging.Events
{
    public interface IAuditAction
    {
        object Action { get; set; }
    }
}