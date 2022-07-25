using System.Collections.Generic;
using System;
using RPA.Abstractions.Enum;

namespace RPA.Abstractions
{
    // Job组dto
    public class JobGroupDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 名称
        public string Name { get; set; }

        #region Monitoring


        // 开始运行时间
        public DateTimeOffset? StartedAt { get; set; }

        // 结束运行时间
        public DateTimeOffset? FinishedAt { get; set; }

        #endregion Monitoring
        // 部门id
        public Guid DepartmentId { get; set; }

        // 公司id
        public Guid CompanyId { get; set; }

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

        // 所属对象/实体id
        public Guid OwnerId { get; set; }

        // Job列表
        public List<JobDTO> Jobs { get; set; }
    }
}