namespace SampleInterfaces;

public record SampleState
{
    /// <summary>
    /// PropertyA.
    /// </summary>
    public string PropertyA { get; set; } = string.Empty;

    /// <summary>
    /// PropertyB.
    /// </summary>
    public string PropertyB { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        var propAValue = this.PropertyA ?? "null";
        var propBValue = this.PropertyB ?? "null";
        return $"PropertyA: {propAValue}, PropertyB: {propBValue}";
    }
}



/// <summary>
/// Actor 提醒器数据
/// </summary>
public class ActorReminderData
{
    public string Name { get; set; } = string.Empty;

    public TimeSpan DueTime { get; set; }

    public TimeSpan Period { get; set; }

    public override string ToString()
    {
        return $"Name: {this.Name}, DueTime: {this.DueTime}, Period: {this.Period}";
    }
}