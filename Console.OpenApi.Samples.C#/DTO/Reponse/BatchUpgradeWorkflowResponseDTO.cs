namespace RPA.Abstractions
{
    // 批量升级流程包结果dto
    public class BatchUpgradeWorkflowResponseDTO
    {
        // 总数
        public int Total { get; set; }

        // 成功数量
        public int SuccessCount { get; set; }

        // 失败数量
        public int FailureCount { get; set; }
    }
}