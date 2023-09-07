using Dapr.Actors;

namespace Common.Domain.Interfaces.Actors;

/// <summary>
/// 
/// </summary>
public interface INumberGeneratorActor : IActor
{
    Task<string> GenerateNumberAsync(int bizType);
}
