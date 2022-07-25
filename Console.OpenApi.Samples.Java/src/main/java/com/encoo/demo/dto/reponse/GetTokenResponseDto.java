package com.encoo.demo.dto.reponse;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
// 获取token响应dto
public class GetTokenResponseDto {
    private String access_token;
}



