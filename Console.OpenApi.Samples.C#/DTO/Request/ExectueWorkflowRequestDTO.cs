namespace RPA.Abstractions
{
    using global::RPA.Abstractions.Enum;
    using System;
    using System.Collections.Generic;

    namespace RPA.Abstractions
    {
        public class ExectueWorkflowRequestDTO
        {
            // 参数
            public IList<PackageVersionArgument> Arguments { get; set; }

            // 视频录制模式
            public VideoRecordMode? VideoRecordMode { get; set; }

            // 优先级
            public int? Priority { get; set; }

            // 最大重试次数
            public int? MaxRetryCount { get; set; }

            // 执行策略
            public JobExecutionPolicy? TriggerPolicy { get; set; }

            #region start up 
            // 启动类型
            public JobStartupType? StartupType { get; set; }

            // 启动资源Id
            public Guid? StartupId { get; set; }

            // 启动资源名
            public string StartupName { get; set; }
            #endregion

            #region Job关联体/生成者,如vicode,流程编排等
            // 外部调用，要求传入触发关联体（用于标识场景）
            // Job关联体id
            public Guid? JobSubjectId { get; set; }

            // Job关联体名称
            public string JobSubjectName { get; set; }

            // Job关联体类型
            public string JobSubjectType { get; set; }
            #endregion
        }
    }
}