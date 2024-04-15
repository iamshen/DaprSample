using Dapr.PluggableComponents.Components;

namespace PluggableComponents;

/// <summary>
/// 自定义实现 PostgreSQL Statestore 组件
/// </summary>
/// <remarks>
/// 实例化一个新的   <see cref="HStatestore"/> 类.
/// </remarks>
/// <param name="logger">logger</param>
/// <exception cref="ArgumentNullException"></exception>
public class HStatestore(ILogger<HStatestore> logger) : IStateStore, IBulkStateStore, ITransactionalStateStore, IQueryableStateStore, IPluggableComponentFeatures
{

    private readonly ILogger<HStatestore> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    // TODO: my  storage( it can be pg mem ...)

    #region StateStore

    public async Task DeleteAsync(StateStoreDeleteRequest request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("DeleteAsync: {request}", request);

        await Task.CompletedTask;
    }

    public async Task<StateStoreGetResponse?> GetAsync(StateStoreGetRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAsync: {request}", request);


        await Task.CompletedTask;


        return default;
    }

    public async Task InitAsync(MetadataRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("InitAsync: {request}", request);


        foreach (var (key, value) in request.Properties)
        {
            _logger.LogInformation("Metadata Properties {key}: {value}", key, value);
        }
        await Task.CompletedTask;
    }

    public async Task SetAsync(StateStoreSetRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SetAsync: {request}", request);


        foreach (var (key, value) in request.Metadata)
        {
            _logger.LogInformation("Metadata {key}: {value}", key, value);
        }

        await Task.CompletedTask;
    }
    #endregion

    #region BulkStateStore


    public async Task BulkDeleteAsync(StateStoreDeleteRequest[] requests, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("StateStoreDeleteRequest: {request}", requests);

        await Task.CompletedTask;
    }

    public async Task<StateStoreBulkStateItem[]> BulkGetAsync(StateStoreGetRequest[] requests, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("BulkGetAsync: {request}", requests);
        await Task.CompletedTask;

        return [];
    }

    public async Task BulkSetAsync(StateStoreSetRequest[] requests, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("BulkSetAsync: {request}", requests);
        await Task.CompletedTask;
    }

    #endregion

    #region TransactionalStateStore

    public async Task TransactAsync(StateStoreTransactRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(message: "TransactAsync: {request}", request);
        // Start transaction...
        try
        {
            foreach (var operation in request.Operations)
            {
                await operation.Visit(
                    async deleteRequest =>
                    {
                        // Process delete request...
                        _logger.LogInformation(message: "TransactAsync: deleteRequest {request}", deleteRequest);


                        await Task.Delay(0);
                    },
                    async setRequest =>
                    {
                        // Process set request...
                        _logger.LogInformation(message: "TransactAsync: setRequest {request}", setRequest);

                        await Task.Delay(0);
                    });
            }
        }
        catch
        {
            // Rollback transaction...
            System.Console.WriteLine("Rollback transaction >>>");
            //throw;
        }
        await Task.CompletedTask;
    }

    #endregion


    #region QueryableStateStore

    public async Task<StateStoreQueryResponse> QueryAsync(StateStoreQueryRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("QueryAsync: {request}", request);


        await Task.CompletedTask;


        return new StateStoreQueryResponse();
    }
    #endregion

    public async Task<string[]> GetFeaturesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await Task.CompletedTask;

        string[] features = { "ETAG", "TRANSACTIONAL" };
        _logger.LogInformation($"Registering State Store Features : {string.Join(",", features)}");
        return features;

    }
}

public static class LoggerExtensions
{
    public static IDisposable BeginNamedScope(this ILogger logger, string name, params ValueTuple<string, object>[] properties)
    {
        var dictionary = properties.ToDictionary(p => p.Item1, p => p.Item2);
        dictionary[name + ".Scope"] = Guid.NewGuid();
        return logger.BeginScope(dictionary);
    }
}