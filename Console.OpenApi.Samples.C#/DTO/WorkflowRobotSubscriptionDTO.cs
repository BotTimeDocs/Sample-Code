using System;

namespace RPA.Abstractions
{
    // 流程部署关联机器人Dto
    public class WorkflowRobotSubscriptionDTO
    {
        // 流程部署Id
        public Guid WorkflowId { get; set; }

        // 机器人Id
        public Guid RobotId { get; set; }

        // 关联失败原因
        public string ErrorMessage { get; set; }
    }
}
