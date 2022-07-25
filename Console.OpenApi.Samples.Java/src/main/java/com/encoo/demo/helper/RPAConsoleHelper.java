package com.encoo.demo.helper;

import com.encoo.demo.Constants;
import com.encoo.demo.dto.reponse.GetTokenResponseDto;
import com.encoo.demo.utils.HttpRequestUtil;
import com.encoo.demo.utils.JsonUtil;
import org.springframework.util.Assert;

import java.util.HashMap;
import java.util.Map;

public class RPAConsoleHelper {
    /**
     * 获取Token
     */
    public static String getToken() {
        String ssoUrl = Constants.SSO_URL;
        String userName = Constants.USER_NAME;
        String pwd = Constants.USER_PWD;
        String params = String.format(
                "client_id=thirdpartyservice" +
                        "&grant_type=password" +
                        "&username=%s" +
                        "&password=%s", userName, pwd);
        Map<String, String> headers = new HashMap();
        headers.put("Content-type", "application/x-www-form-urlencoded");
        headers.put("DataEncoding", "UTF-8");
        String resText = HttpRequestUtil.doPost(
                ssoUrl,
                headers,
                params);
        GetTokenResponseDto dto = (GetTokenResponseDto) JsonUtil.jsonToObject(resText, GetTokenResponseDto.class);

        // 操作成功
        Assert.hasLength(dto.getAccess_token(), "Token长度大于0");
        return dto.getAccess_token();
    }
}
