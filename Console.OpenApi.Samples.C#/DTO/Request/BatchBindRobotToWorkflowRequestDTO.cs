using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 批量关联机器人到流程部署
    public class BatchBindRobotToWorkflowRequestDTO
    {
        // 机器人id列表
        public IList<Guid> RobotIds { get; set; } = new List<Guid>();
    }
}
