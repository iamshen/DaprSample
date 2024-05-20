using Dapr.Actors;
using Dapr.Actors.Runtime;
using DaprTool.BuildingBlocks.Domain.EventBus;
using DaprTool.BuildingBlocks.Domain.Events;
using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.Domain.Actors;

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
    public string StateDataKey => GetType().Name;

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
    ///     方法被执行之前
    /// </summary>
    /// <param name="actorMethodContext"></param>
    /// <returns></returns>
    protected override Task OnPreActorMethodAsync(ActorMethodContext actorMethodContext)
    {
        Logger.LogInformation("On Pre ActorMethod: {MethodName}", actorMethodContext.MethodName);

        return base.OnPreActorMethodAsync(actorMethodContext);
    }

    /// <summary>
    ///     方法被执行之后
    /// </summary>
    /// <param name="actorMethodContext"></param>
    /// <returns></returns>
    protected override Task OnPostActorMethodAsync(ActorMethodContext actorMethodContext)
    {
        Logger.LogInformation("On Post ActorMethod: {MethodName}", actorMethodContext.MethodName);

        return base.OnPreActorMethodAsync(actorMethodContext);
    }

    /// <summary>
    ///     方法执行失败时
    /// </summary>
    /// <param name="actorMethodContext"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected override Task OnActorMethodFailedAsync(ActorMethodContext actorMethodContext, Exception ex)
    {
        Logger.LogInformation("On Actor MethodFailed: {MethodName}", actorMethodContext.MethodName);
        Logger.LogError(ex, nameof(OnActorMethodFailedAsync));

        return base.OnPreActorMethodAsync(actorMethodContext);
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

    /// <summary>
    ///     获取序列号生成器
    /// </summary>
    /// <returns></returns>
    public ISerialNumberActor GetSerialNumberService()
    {
        var orderNumberActorId = new ActorId(Constants.DefaultActorId);

        var orderNumberProxy =
            ProxyFactory.CreateActorProxy<ISerialNumberActor>(orderNumberActorId, nameof(SerialNumberActor));

        return orderNumberProxy;
    }
}