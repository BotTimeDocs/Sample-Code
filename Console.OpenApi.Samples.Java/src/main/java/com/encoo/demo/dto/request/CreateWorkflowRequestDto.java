package com.encoo.demo.dto.request;

import com.encoo.demo.dto.Argument;
import lombok.Data;

@Data
// 创建流程部署请求dto
public class CreateWorkflowRequestDto {
    // 名称
    private String name;

    // 流程包id
    private String packageId;

    // 流程包版本id
    private String packageVersionId;

    // 允许重试的最大次数
    private int maxRetryCount = 0;

    // 优先级
    private int priority = 2000;

    private String videoRecordMode = "NeverRecord"; // 视频录制模式，NeverRecord-不录制

    // 参数
    private Argument[] arguments;

    // 执行目标机器人组id
    private String  associatedQueueId;
}

