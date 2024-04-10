namespace SampleInterfaces;

public class OverdraftException : Exception
{
    public OverdraftException(decimal balance, decimal amount)
        : base($"您当前的余额是{balance:c}，无法提取{amount:c}。")
    {
    }
}
