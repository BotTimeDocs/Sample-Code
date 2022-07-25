using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    public class QueueDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 机器人组名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 标记（列表）
        public IList<string> Tags { get; set; }

        // 附加属性
        public IDictionary<string, string> Properties { get; set; }

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

        // 包含机器人数量
        public int? RobotCount { get; set; }

        // 公司id
        public Guid CompanyId { get; set; }

        // 部门id
        public Guid DepartmentId { get; set; }
    }
}