package com.encoo.demo.dto;

import lombok.Data;

@Data
public class Argument {
    // 参数名称
    private String name;

    // 参数类型
    private String type;

    // 参数方向
    private String direction="In"; // or Out/InOut

    // 参数默认值
    private String defaultValue;
}
