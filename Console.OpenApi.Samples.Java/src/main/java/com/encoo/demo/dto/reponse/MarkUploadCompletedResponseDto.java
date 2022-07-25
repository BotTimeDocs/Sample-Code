package com.encoo.demo.dto.reponse;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class MarkUploadCompletedResponseDto {
    // 流程包id
    private String packageId;

    // 流程包版本id
    private String packageVersionId;

    // 流程包名称
    private String packageName;

    // 最新版本
    private String lastVersion;
}

