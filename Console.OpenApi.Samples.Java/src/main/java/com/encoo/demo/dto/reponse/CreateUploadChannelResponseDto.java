package com.encoo.demo.dto.reponse;

import com.encoo.demo.dto.Channel;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
// 创建上传通过响应dto
public class CreateUploadChannelResponseDto {
    // 主键id
    private String id;

    // 上传通道
    private Channel channel;
}

