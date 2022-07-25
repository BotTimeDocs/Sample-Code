using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 批量升级流程包请求
    public class BatchUpgradeWorkflowRequestDTO
    {
        // 流程部署id列表
        public IList<Guid> WorkflowIds { get; set; }
    }
}