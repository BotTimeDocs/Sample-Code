using OpenApi.Sdk.Utils;
using RPA.Abstractions;
using RPA.Abstractions.RPA.Abstractions;
using System;
using System.Collections.Generic;

namespace OpenApi.Samples.Ops
{
    public partial class ConsoleOps
    {
        public PagedListResponseDTO<WorkflowDTO> QueryWorkflows(PagedListRequestDTO pagedListRequest)
        {
            var url = $"{API_BASE}/openapi/workflows?" + BuildQueryString(pagedListRequest);
            return HttpUtility.Get<PagedListResponseDTO<WorkflowDTO>>(url, GetHeaders());
        }

        // 创建流程部署
        public WorkflowDTO CreateWorkflow(CreateWorkflowRequestDTO createWorkflowRequest)
        {
            var url = $"{API_BASE}/openapi/workflows";
            return HttpUtility.Post<WorkflowDTO>(url, GetHeaders(), createWorkflowRequest.SerializeObject());
        }

        // 更新流程部署
        public WorkflowDTO UpdateWorkflow(Guid workflowId, UpdateWorkflowRequestDTO updateWorkflowRequest)
        {
            var url = $"{API_BASE}/openapi/workflows/{workflowId}";
            return HttpUtility.Patch<WorkflowDTO>(url, GetHeaders(), updateWorkflowRequest.SerializeObject());
        }

        // 更新流程部署到最新的流程包版本
        public BatchUpgradeWorkflowResponseDTO BatchUpgradeWorkflows(BatchUpgradeWorkflowRequestDTO requestDTO)
        {
            var url = $"{API_BASE}/openapi/workflows/batch/upgrade";
            return HttpUtility.Post<BatchUpgradeWorkflowResponseDTO>(url, GetHeaders(), requestDTO.SerializeObject());
        }

        // 删除流程部署
        public void DeleteWorkflow(Guid workflowId)
        {
            var url = $"{API_BASE}/openapi/workflows/{workflowId}";
            HttpUtility.Delete(url, GetHeaders());
        }

        // 执行流程部署
        public List<JobDTO> ExecuteWorkflow(Guid id, ExectueWorkflowRequestDTO exectueWorkflowRequest)
        {
            var url = $"{API_BASE}/openapi/workflows/{id}/execute";
            return HttpUtility.Post<List<JobDTO>>(url, GetHeaders(), exectueWorkflowRequest.SerializeObject());
        }

        // 流程部署批量关联机器人
        public List<WorkflowRobotSubscriptionDTO> BindRobotsToWorkflow(Guid id, BatchBindRobotToWorkflowRequestDTO batchBindRobot)
        {
            var url = $"{API_BASE}/openapi/workflows/{id}/robots";
            return HttpUtility.Post<List<WorkflowRobotSubscriptionDTO>>(url, GetHeaders(), batchBindRobot.SerializeObject());
        }

        // 移除流程部署关联的机器人
        public void UnBindRobotToWorkflow(Guid id, Guid robotId)
        {
            var url = $"{API_BASE}/openapi/workflows/{id}/robots/{robotId}";
            HttpUtility.Delete(url, GetHeaders());
        }

        // 获取流程部署关联机器人列表
        public PagedListResponseDTO<RobotDTO> QueryWorkflowBindedRobots(Guid id)
        {
            var url = $"{API_BASE}/openapi/workflows/{id}/robots";
            return HttpUtility.Get<PagedListResponseDTO<RobotDTO>>(url, GetHeaders());
        }
    }
}