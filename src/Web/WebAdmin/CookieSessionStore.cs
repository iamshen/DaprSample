using Dapr.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

internal class CookieSessionStore : ITicketStore
{
    string DAPR_STORE_NAME = "dt-statestore";
    string KeyPrefix = "auth-session-store-";
    private DaprClient _client = new DaprClientBuilder().Build();

    public async Task RemoveAsync(string key)
    {
        await _client.DeleteStateAsync(DAPR_STORE_NAME, string.Concat(KeyPrefix, key));
    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        byte[] value = SerializeToBytes(ticket);

        await _client.SaveStateAsync(DAPR_STORE_NAME, key, value, metadata: new Dictionary<string, string>()
        {
            {"ttlInSeconds", (60 * 1 * 60).ToString()},
        });

    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var bytes = await _client.GetStateAsync<byte[]>(DAPR_STORE_NAME, key);
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

