using Dapr.Actors;
using Dapr.Actors.Client;
using SampleInterfaces;

Console.WriteLine("starting sapmle client...");


var data = new SampleState()
{
    PropertyA = "ValueA",
    PropertyB = "ValueB",
};

// 创建一个 actor Id.
var actorId = ActorId.CreateRandom();

// 使用强类型调用
var proxy = ActorProxy.Create<ISampleActor>(actorId, "SampleActor");

Console.WriteLine("调用 actor proxy 保存数据");
await proxy.SaveData(data, TimeSpan.FromMinutes(10));
Console.WriteLine("调用 actor proxy 获取数据");
var receivedData = await proxy.GetData();
Console.WriteLine($"接收到数据 {receivedData}.");

// try
// {
//     Console.WriteLine("调用一个没有参数和返回类型的 actor 方法");
//     await proxy.TestNoArgumentNoReturnType();
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"ERROR: 在调用无参数和无返回类型的方法时遇到异常。 Exception: {ex}");
// }

// try
// {
//     await proxy.TestThrowException();
// }
// catch (ActorMethodInvocationException ex)
// {
//     if (ex.InnerException is ActorInvokeException invokeEx && invokeEx.ActualExceptionType is "System.NotImplementedException")
//     {
//         Console.WriteLine($"得到正确的异常");
//     }
//     else
//     {
//         Console.WriteLine($"得到不正确的异常. Exception {ex.InnerException}");
//     }
// }

// // 在没有Remoting的情况下进行调用，这显示了使用InvokeMethodAsync方法进行方法调用，
// // 方法名称及其有效请求体作为参数提供给InvokeMethodAsync方法。
// Console.WriteLine("Making calls without Remoting.");
// var nonRemotingProxy = ActorProxy.Create(actorId, "SampleActor");
// await nonRemotingProxy.InvokeMethodAsync("TestNoArgumentNoReturnType");
// await nonRemotingProxy.InvokeMethodAsync("SaveData", data);
// var res = await nonRemotingProxy.InvokeMethodAsync<SampleState>("GetData");

// Console.WriteLine("注册提醒器和定时器");
// await proxy.RegisterTimer();
// await proxy.RegisterReminder();
// Console.WriteLine("等待 6 秒 观察计时器和提醒 触发");
// await Task.Delay(6000);

// Console.WriteLine("在计时器和提醒触发后，使用 actor proxy 调用以获取数据");
// receivedData = await proxy.GetData();
// Console.WriteLine($"返回的数据: {receivedData}.");

// Console.WriteLine("获取已注册提醒器的详细信息");
// var reminder = await proxy.GetReminder();
// Console.WriteLine($"提醒器: {reminder}.");

// Console.WriteLine("注销计时器。如果 actor 作为 Dapr垃圾收集的一部分被停用 计时器将以任何方式停止。");
// await proxy.UnregisterTimer();
// Console.WriteLine("注销提醒器。提醒是持久的，并且在显式取消注册或删除参与者之前不会停止。");
// await proxy.UnregisterReminder();

// Console.WriteLine("注册重复提醒-提醒将重复3次。");
// await proxy.RegisterReminderWithRepetitions(3);
// Console.WriteLine("等待提醒被触发");
// await Task.Delay(5000);
// Console.WriteLine("获取已注册提醒器的详细信息");
// reminder = await proxy.GetReminder();
// Console.WriteLine($"提醒器： {reminder?.ToString() ?? "None"} (expecting None).");
// Console.WriteLine("使用ttl和重复注册提醒 即当满足任一条件时提醒停止-提醒将重复2次。");
// await proxy.RegisterReminderWithTtlAndRepetitions(TimeSpan.FromSeconds(5), 2);
// Console.WriteLine("获取已注册提醒器的详细信息");
// reminder = await proxy.GetReminder();
// Console.WriteLine($"提醒器： {reminder}.");
// Console.WriteLine("注销提醒。提醒是持久的，并且在显式取消注册或删除参与者之前不会停止。");
// await proxy.UnregisterReminder();

// Console.WriteLine("注册提醒和定时器与TTL -提醒将在10秒后自行删除。");
// await proxy.RegisterReminderWithTtl(TimeSpan.FromSeconds(10));
// await proxy.RegisterTimerWithTtl(TimeSpan.FromSeconds(10));
// Console.WriteLine("获取已注册提醒器的详细信息");
// reminder = await proxy.GetReminder();
// Console.WriteLine($"提醒器： {reminder}.");

// // Track the reminder.
// var timer = new Timer(async state => Console.WriteLine($"数据: {await proxy.GetData()}"), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
// await Task.Delay(TimeSpan.FromSeconds(21));
// await timer.DisposeAsync();

// Console.WriteLine("创建 Bank 示例 Actor 中...");
// var bank = ActorProxy.Create<IBankActor>(ActorId.CreateRandom(), "SampleActor");
// while (true)
// {
//     var balance = await bank.GetAccountBalance();
//     Console.WriteLine($"账户 '{balance.AccountId}' 余额 '{balance.Balance:c}'.");

//     Console.WriteLine($"取款 '{10m:c}'...");
//     try
//     {
//         await bank.Withdraw(new WithdrawRequest() { Amount = 10m, });
//     }
//     catch (ActorMethodInvocationException ex)
//     {
//         Console.WriteLine("透支额度: " + ex.Message);
//         break;
//     }
// }