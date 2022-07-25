namespace RPA.Abstractions.Enum
{
    // 视频录制模式
    public enum VideoRecordMode
    {
        // 从不录制
        NeverRecord = 0,

        // 仅成功时上传
        ReportOnlyWhenSucceeded = 1,

        // 仅失败时上传
        ReportOnlyWhenFailed = 2,

        // 总是录制
        AlwaysRecord = 3,

        // 总是上传
        AlwaysReport = 4
    }
}