using RPA.Abstractions.Enum;
using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 流程部署Dto
    public class WorkflowDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 部门Id
        public Guid DepartmentId { get; set; }

        // 流程部署名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 参数（列表）
        public IList<PackageVersionArgument> Arguments { get; set; } = new List<PackageVersionArgument>();

        // 标记（列表）
        public IList<string> Tags { get; set; }

        // 视频录制模式
        public VideoRecordMode VideoRecordMode { get; set; }

        #region Package
        // 流程包名称
        public string PackageName { get; set; }

        // 流程包id
        public Guid PackageId { get; set; }

        // 流程包版本id
        public Guid PackageVersionId { get; set; }

        // 流程包版本号
        public string PackageVersion { get; set; }

        #endregion

        #region Queue

        // （执行目标）机器人组id
        public Guid? AssociatedQueueId { get; set; }

        #endregion

        #region Policy

        // 优先级
        public int Priority { get; set; }

        // （运行失败时）最大重试次数
        public int MaxRetryCount { get; set; }

        // 执行策略
        public JobExecutionPolicy TriggerPolicy { get; set; } = JobExecutionPolicy.AnyTime;

        #endregion

        // 最后触发时间
        public DateTimeOffset? LastTriggeredAt { get; set; }

        // 创建时间
        public DateTimeOffset CreatedAt { get; set; }

        // 创建人id
        public Guid CreatedBy { get; set; }

        // 创建人名称
        public string CreatedByName { get; set; }

        // 更新时间
        public DateTimeOffset? ModifiedAt { get; set; }

        // 更新人id
        public Guid? ModifiedBy { get; set; }

        // 更新人名称
        public string ModifiedByName { get; set; }

        // 所属公司id
        public Guid CompanyId { get; set; }
    }
}