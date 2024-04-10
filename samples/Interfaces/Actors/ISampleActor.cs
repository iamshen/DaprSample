using Dapr.Actors;
using System;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;

namespace SampleInterfaces;



public interface ISampleActor : IActor
{

    /// <summary>
    /// 保存 data
    /// </summary>
    /// <param name="state">state</param>
    /// <param name="ttl">TTL of state key.</param>
    /// <returns></returns>
    Task SaveData(SampleState state, TimeSpan ttl);

    /// <summary>
    /// 获取 data
    /// </summary>
    /// <returns></returns>
    Task<SampleState> GetData();

    /// <summary>
    ///  测试方法： 抛出异常方法
    /// </summary>
    /// <returns></returns>
    Task TestThrowException();

    /// <summary>
    /// 测试方法：用于验证没有参数和返回类型的方法调用
    /// </summary>
    /// <returns></returns>
    Task TestNoArgumentNoReturnType();

    /// <summary>
    /// 注册提醒器
    /// </summary>
    /// <returns></returns>
    Task RegisterReminder();

    /// <summary>
    /// 注册提醒器
    /// </summary>
    /// <param name="ttl">指定提醒何时过期的时间跨度.</param>
    /// <returns></returns>
    Task RegisterReminderWithTtl(TimeSpan ttl);

    /// <summary>
    /// 注销已注册的提醒器。
    /// </summary>
    /// <returns>Task representing the operation.</returns>
    Task UnregisterReminder();

    /// <summary>
    /// 注册一个计时器。
    /// </summary>
    /// <returns></returns>
    Task RegisterTimer();

    /// <summary>
    /// 注册一个计时器。
    /// </summary>
    /// <param name="ttl">可选TimeSpan，指定计时器何时到期。</param>
    /// <returns></returns>
    Task RegisterTimerWithTtl(TimeSpan ttl);

    /// <summary>
    /// 注册一个重复的提醒器
    /// </summary>
    /// <param name="repetitions">应该调用提醒器的重复次数</param>
    /// <returns></returns>
    Task RegisterReminderWithRepetitions(int repetitions);

    /// <summary>
    /// 注册一个重复的提醒器
    /// </summary>
    /// <param name="ttl">指定计时器何时到期的TimeSpan</param>
    /// <param name="repetitions">应该调用提醒的重复次数。</param>
    /// <returns></returns>
    Task RegisterReminderWithTtlAndRepetitions(TimeSpan ttl, int repetitions);

    /// <summary>
    /// 获取已注册的提醒
    /// </summary>
    /// <returns></returns>
    Task<ActorReminderData> GetReminder();

    /// <summary>
    /// 注销已注册的计时器。
    /// </summary>
    /// <returns></returns>
    Task UnregisterTimer();
}