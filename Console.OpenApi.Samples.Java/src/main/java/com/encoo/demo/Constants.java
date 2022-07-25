package com.encoo.demo;

public class Constants {
    public static final String Queue_ID = "30f57ce8-2530-4d30-b9d1-f6352a0381c1"; // 控制台中存在的机器人组(id)
    // 登录接口地址，需将10.10.30.43替换为自己服务器的IP,端口默认为81
    public static final String SSO_URL = "http://10.10.30.43:81/connect/token";
    public static final String USER_NAME = "admin@encootech.com";
    public static final String USER_PWD = "123456";

    // API接口地址，需将10.10.30.43替换为自己服务器的IP,端口默认为8080
    public static final String API_BASE = "http://10.10.30.43:8080";

    // 从网页接口调用中获取，CompanyId、DepartmentId对于同一用户是固定的
    public static final String COMPANY_ID = "9a0c7d89-404a-4896-a0b9-1add6d9551e8";
    public static final String DEPARTMENT_ID = "2836f935-3c0b-4ce3-8cc3-77324f8df68c";
}