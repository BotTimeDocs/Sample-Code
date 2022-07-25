package com.encoo.demo.dto;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
// 流程部署Dto
public class WorkflowDto {
    // 主键
    private String id;
    // 流程部署名称
    private String name;
    // ... 其它属性
}