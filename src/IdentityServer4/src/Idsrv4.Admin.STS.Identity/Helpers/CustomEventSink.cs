using IdentityServer4.Events;
using IdentityServer4.Services;
using Serilog.Core;
using System.Threading.Tasks;

namespace Idsrv4.Admin.STS.Identity.Helpers
{
    public class CustomEventSink: IEventSink
    {
        private readonly Logger _log;

        public CustomEventSink()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [111111111] [{Level:u3}] {Message:lj} {NewLine}{Exception}")
                .CreateLogger();
        }

        public Task PersistAsync(Event evt)
        {
            if (evt.EventType == EventTypes.Success ||
                evt.EventType == EventTypes.Information)
            {
                _log.Information("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);
            }
            else
            {
                _log.Error("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);
            }

            return Task.CompletedTask;
        }
    }
}
