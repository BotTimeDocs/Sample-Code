namespace RPA.Abstractions.Enum
{
    // 任务执行策略
    public enum JobExecutionPolicy
    {
        // 当无可用机器人时取消执行
        SkipWhenNoResource,

        // 不应用策略-无可用机器人时等待
        AnyTime,
    }
}