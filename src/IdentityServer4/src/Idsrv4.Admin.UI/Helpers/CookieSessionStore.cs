using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;

#nullable enable

namespace Idsrv4.Admin.UI.Helpers
{

    internal class CookieSessionStore : ITicketStore
    {
        string DAPR_STORE_NAME = "dt-statestore";
        string KeyPrefix = "auth-session-store-";
        private DaprClient _client = new DaprClientBuilder().Build();
        private IMemoryCache _memoryCache =  new MemoryCache(new MemoryCacheOptions());

        public async Task RemoveAsync(string key)
        {
            if (_client is null)
            {
                _memoryCache.Remove(key);

                await Task.CompletedTask;
            }
            else
            {
                await _client.DeleteStateAsync(DAPR_STORE_NAME, string.Concat(KeyPrefix, key));
            }
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            byte[] value = SerializeToBytes(ticket);

            if (_client is null)
            {
                _memoryCache.Set(key, value);
            }
            else
            {
                await _client.SaveStateAsync(DAPR_STORE_NAME, key, value, metadata: new Dictionary<string, string>()
                {
                    {"ttlInSeconds", (60 * 1 * 60).ToString()},
                });
            }

        }

        public async Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            byte[]? bytes;

            if (_client is null)
            {
                _memoryCache.TryGetValue(key, out bytes);
            }
            else
            {
                bytes = await _client.GetStateAsync<byte[]>(DAPR_STORE_NAME, key);
            }

            if (bytes is null) return default;

            var value = DeserializeFromBytes(bytes);
            return value;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = $"{KeyPrefix}{Guid.NewGuid()}";
            await RenewAsync(key, ticket);
            return key;
        }

        private static byte[] SerializeToBytes(AuthenticationTicket source)
        {
            return TicketSerializer.Default.Serialize(source);
        }

        private static AuthenticationTicket? DeserializeFromBytes(byte[] source)
        {
            return source == null ? default : TicketSerializer.Default.Deserialize(source);
        }
    }
}
