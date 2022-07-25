using RPA.Abstractions.Enum;
using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    public class JobDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 流程部署id
        public Guid WorkflowId { get; set; }

        // 名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        #region Package

        // 流程包id
        public Guid PackageId { get; set; }

        // 流程包名称
        public string PackageName { get; set; }

        // 流程包版本号
        public string PackageVersion { get; set; }

        // 流程包版本id
        public Guid PackageVersionId { get; set; }

        // 流程运行参数
        public IList<PackageVersionArgument> Arguments { get; set; }

        #endregion

        #region Scheduling
        // 执行目标机器人组id
        public Guid ContainingQueueId { get; set; }

        // 优先级,0 - 5000
        public int Priority { get; set; }

        // Job显示状态
        public JobState DisplayState { get; set; }

        // 视频录制模式
        public VideoRecordMode VideoRecordMode { get; set; }

        // 异常原因说明
        public string Message { get; set; }

        /// <summary>
        /// 标记为删除：1 - 是，0 - 否
        /// </summary>
        public bool Deleted { get; set; }
        #endregion

        #region runinstance
        // 最后一次执行实例id
        public Guid? LastRunInstaceId { get; set; }

        // 最后执行的机器人名称
        public string LastRobotName { get; set; }

        #endregion

        #region Monitoring
        // 开始执行时间
        public DateTimeOffset? StartedAt { get; set; }

        // 结束执行时间
        public DateTimeOffset? FinishedAt { get; set; }

        // 重试计数
        public int RetriedCount { get; set; }

        // 允许的最大重试次数
        public int MaxRetryCount { get; set; }

        #endregion Monitoring
        // 部门id
        public Guid DepartmentId { get; set; }

        // 公司id
        public Guid CompanyId { get; set; }

        // 定时器id,定时器触发时才有值
        public Guid? CronTriggerId { get; set; }

        // 执行策略
        public JobExecutionPolicy? JobExecutionPolicy { get; set; }

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

        #region start up
        // 启动方式
        public JobStartupType? JobStartupType { get; set; }
        // 启动者id
        public Guid? JobStartupId { get; set; }
        // 启动者名称
        public string JobStartupName { get; set; }

        #endregion

        #region Job Group
        // Jobgroup Id
        public Guid JobGroupId { get; set; }

        // 执行次序
        public int SeqNum { get; set; }

        // Step展示，如1，2-1，2-2，3...
        public string Step { get; set; }
        #endregion

        #region Job关联体/生成者
        // Job关联体id
        public Guid? JobSubjectId { get; set; }

        // Job关联体名称
        public string JobSubjectName { get; set; }

        // Job关联体类型
        public string JobSubjectType { get; set; }
        #endregion
    }
}
