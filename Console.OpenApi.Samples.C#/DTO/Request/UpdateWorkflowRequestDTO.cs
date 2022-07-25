namespace RPA.Abstractions
{
    //更新流程部署请求dto
    public class UpdateWorkflowRequestDTO
    {
        // 名称
        public string Name { get; set; }

        // ...

        // 优先级
        public int? Priority { get; set; }

        // 失败重试次数
        public int? MaxRetryCount { get; set; }
    }
}