namespace RPA.Abstractions.Enum
{
    // Job状态
    public enum JobState
    {
        // 等待中
        Queued = 0,

        // 运行中
        Running,

        // 失败
        Failed,

        // 已取消
        Cancelled,

        // 成功
        Succeeded,

        // 已暂停
        Paused
    }
}