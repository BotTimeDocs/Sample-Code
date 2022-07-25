package com.encoo.demo.dto;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Data;

import java.util.Map;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
// 文件上传通道
public class Channel {
    // 上传地址
    private String uri;

    // 上传请求的Headers
    private Map<String, String> headers;
}
