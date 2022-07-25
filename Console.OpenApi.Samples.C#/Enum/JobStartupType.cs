namespace RPA.Abstractions.Enum
{
    // Job启动类型
    public enum JobStartupType
    {
        // 手工
        Manually,
        // 定时器触发
        Trigger,
        // 任务队列触发
        TaskQueue
    }
}