package com.encoo.demo.dto.request;

import com.encoo.demo.dto.Argument;
import lombok.Data;

@Data
public class ExecuteWorkflowRequestDto {
    // 最大重试次数
    private int maxRetryCount;

    // Job关联体id
    private String JobSubjectId = "1dff6f91-d4e1-4c75-a7a9-1de4f47a51c4";

    // Job关联体名称
    private String JobSubjectType = "test_type";

    // Job关联体类型
    private String JobSubjectName = "ww_test";

    // 优先级
    private int priority;

    // 参数
    private Argument[] arguments;

    // 视频录制模式
    private String videoRecordMode = "NeverRecord";
}
