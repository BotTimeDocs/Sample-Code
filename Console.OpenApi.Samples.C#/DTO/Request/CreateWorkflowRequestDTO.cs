using RPA.Abstractions.Enum;
using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 创建流程部署请求dto
    public class CreateWorkflowRequestDTO
    {
        // 名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 标记
        public IList<string> Tags { get; set; }

        // 参数
        public IList<PackageVersionArgument> Arguments { get; set; } = new List<PackageVersionArgument>();

        // 附加属性
        public IDictionary<string, string> Properties { get; set; }

        // 视频录制模式
        public VideoRecordMode VideoRecordMode { get; set; }

        #region Package
        // 流程包名称
        public string PackageName { get; set; }

        // 流程包id
        public Guid PackageId { get; set; }

        // 流程包版本id
        public Guid PackageVersionId { get; set; }
        #endregion

        #region Queue

        // 执行目标机器人组id
        public Guid? AssociatedQueueId { get; set; }

        #endregion

        #region Policy

        // 优先级
        public int Priority { get; set; }

        // 允许重试的最大次数
        public int? MaxRetryCount { get; set; } = 3;

        // 执行策略
        public JobExecutionPolicy TriggerPolicy { get; set; } = JobExecutionPolicy.AnyTime;

        #endregion
    }
}