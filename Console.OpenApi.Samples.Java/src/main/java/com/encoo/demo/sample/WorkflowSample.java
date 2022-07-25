package com.encoo.demo.sample;

import com.encoo.demo.Constants;
import com.encoo.demo.dto.JobDto;
import com.encoo.demo.dto.WorkflowDto;
import com.encoo.demo.dto.request.CreateWorkflowRequestDto;
import com.encoo.demo.dto.request.ExecuteWorkflowRequestDto;
import com.encoo.demo.dto.request.UpdateWorkflowRequestDto;
import com.encoo.demo.helper.RPAConsoleHelper;
import com.encoo.demo.utils.HttpRequestUtil;
import com.encoo.demo.utils.JsonUtil;
import org.apache.log4j.Logger;
import org.springframework.util.Assert;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class WorkflowSample {
    private String packageId;
    private String packageVersionId;
    private String robotOrQueueId;

    public WorkflowSample(String packageId, String packageVersionId, String robotOrQueueId) {
        this.packageId = packageId;
        this.packageVersionId = packageVersionId;
        this.robotOrQueueId = robotOrQueueId;
    }

    private static Logger logger = Logger.getLogger(WorkflowSample.class);

    /**
     * 创建流程部署并执行，更新流程部署
     */
    public void testCreateWorkflowAndExecution() {
        String apiBase = Constants.API_BASE;
        String companyId = Constants.COMPANY_ID;
        String departmentId = Constants.DEPARTMENT_ID;

        /** 获取用户Token */
        String token = RPAConsoleHelper.getToken();
        Assert.hasLength(token, "获取用户Token-成功");

        /** 1.创建流程部署 */
        Map<String, String> headers = new HashMap();
        headers.put("Authorization", "Bearer " + token);
        headers.put("Content-type", "application/json");
        headers.put("DataEncoding", "UTF-8");
        headers.put("CompanyId", companyId);
        headers.put("DepartmentId", departmentId);

        WorkflowDto workflow = createWorkflow(apiBase, headers, packageId, packageVersionId, robotOrQueueId
        );
        Assert.hasLength(workflow.getId(), "成功创建Workflow");

        /** 2.执行流程部署 */
        String workflowId = workflow.getId();
        JobDto[] jobs = executeWorkflow(apiBase, headers, workflowId);
        Assert.isTrue(jobs.length > 0, "返回Job数量大于0");

        /** 3.更新流程部署信息 */
        String newName = "Workflow_Test_" + new Date().getTime() + "_Updated";
        WorkflowDto workflowDto = updateWorkflow(apiBase, headers, workflowId, newName);
        Assert.isTrue(newName.equals(workflowDto.getName()), "更新后的名称为：" + workflowDto.getName());
    }

    /**
     * API调用-创建流程部署
     */
    private WorkflowDto createWorkflow(String apiBase, Map<String, String> headers, String packageId, String packageVersionId,
                                       String queueOrRobotId) {
        logger.info("【创建流程部署】开始");
        CreateWorkflowRequestDto createDto = new CreateWorkflowRequestDto();
        createDto.setName("Workflow_Test_" + new Date().getTime());
        createDto.setPackageId(packageId);
        createDto.setPackageVersionId(packageVersionId);
        createDto.setAssociatedQueueId(queueOrRobotId);
        // 设置其它参数略
        String resText = HttpRequestUtil.doPost(
                apiBase + "/openapi/workflows",
                headers,
                JsonUtil.toJsonString(createDto));
        WorkflowDto dto = (WorkflowDto) JsonUtil.jsonToObject(resText, WorkflowDto.class);
        logger.info("【创建流程部署】结束");
        return dto;
    }

    /**
     * API调用-手动执行流程部署
     */
    private JobDto[] executeWorkflow(String apiBase, Map<String, String> headers, String workflowId) {
        logger.info("【执行流程部署】开始");
        String resText = HttpRequestUtil.doPost(
                apiBase + "/openapi/workflows/" + workflowId + "/execute",
                headers,
                JsonUtil.toJsonString(new ExecuteWorkflowRequestDto()));
        JobDto[] dto = (JobDto[]) JsonUtil.jsonToObject(resText, JobDto[].class);
        logger.info("【执行流程部署】结束");
        return dto;
    }

    /**
     * API调用-更新流程部署信息
     */
    private WorkflowDto updateWorkflow(String apiBase, Map<String, String> headers, String workflowId, String newName) {
        logger.info("【更新流程部署】开始");
        UpdateWorkflowRequestDto requestDto = new UpdateWorkflowRequestDto();
        requestDto.setName(newName);
        String resText = HttpRequestUtil.doPatch(
                apiBase + String.format("/openapi/workflows/%s", workflowId),
                headers,
                JsonUtil.toJsonString(requestDto));
        WorkflowDto dto = (WorkflowDto) JsonUtil.jsonToObject(resText, WorkflowDto.class);
        logger.info("【更新流程部署】结束");
        return dto;
    }
}