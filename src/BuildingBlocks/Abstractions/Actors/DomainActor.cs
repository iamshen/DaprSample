using Dapr.Actors.Runtime;
using DaprTool.BuildingBlocks.Abstractions.EventBus;
using DaprTool.BuildingBlocks.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.Abstractions.Actors;

/// <summary>
///     Actor
/// </summary>
public abstract class DomainActor<TState> : Actor
        where TState : class, new()
{
    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="host"></param>
    /// <param name="serviceProvider"></param>
    protected DomainActor(ActorHost host, IServiceProvider serviceProvider) : base(host)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>  StateDataKey </summary>
    public abstract string StateDataKey { get; }

    /// <summary>
    ///     服务提供者
    /// </summary>
    protected IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    ///     ActorId
    /// </summary>
    protected string ActorId => Id.GetId();

    /// <summary>
    ///     每当激活一个 actor 时，都会调用该方法
    ///     当首次调用一个 actor 的任何方法时，该 actor 就会被激活
    /// </summary>
    protected override Task OnActivateAsync()
    {
        Logger.LogDebug("Actor 已激活 ActorId: {Id}", ActorId);

        return Task.CompletedTask;
    }

    /// <summary>
    ///     每当 actor 在一段时间的非活动状态后被停用时，都会调用该方法
    /// </summary>
    protected override Task OnDeactivateAsync()
    {
        Logger.LogDebug("Actor 已失活 ActorId: {Id}", ActorId);

        return Task.CompletedTask;
    }

    /// <summary>
    ///     发布事件
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    protected Task RaiseEvents(params IntegrationEvent[] events)
    {
        var eventBus = ServiceProvider.GetRequiredService<IEventBus>();

        return eventBus.RaiseAsync(events);
    }
}