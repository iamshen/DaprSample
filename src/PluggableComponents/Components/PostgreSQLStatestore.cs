using Dapr.PluggableComponents.Adaptors;
using Dapr.PluggableComponents.Components;
using Dapr.PluggableComponents.Components.StateStore;

namespace PluggableComponents;

/// <summary>
/// 自定义实现 PostgreSQL Statestore 组件
/// </summary>
/// <remarks>
/// 实例化一个新的   <see cref="PostgreSQLStatestore"/> 类.
/// </remarks>
/// <param name="logger">logger</param>
/// <exception cref="ArgumentNullException"></exception>
public class PostgreSQLStatestore(ILogger<PostgreSQLStatestore> logger) : IStateStore //, IBulkStateStore, ITransactionalStateStore, IQueryableStateStore
{

    private readonly ILogger<PostgreSQLStatestore> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    // TODO: npgsql connection storage

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

}
