package com.encoo.demo.sample;

import com.encoo.demo.Constants;
import com.encoo.demo.dto.Channel;
import com.encoo.demo.dto.reponse.CreateUploadChannelResponseDto;
import com.encoo.demo.dto.reponse.MarkUploadCompletedResponseDto;
import com.encoo.demo.helper.RPAConsoleHelper;
import com.encoo.demo.utils.HttpRequestUtil;
import com.encoo.demo.utils.JsonUtil;
import org.apache.log4j.Logger;
import org.springframework.util.Assert;

import java.io.File;
import java.util.HashMap;
import java.util.Map;

public class PackageSample {
    private static Logger logger = Logger.getLogger(PackageSample.class);

    private String packageId;

    public String getPackageId() {
        return packageId;
    }

    private String packageVersionId;

    public String getPackageVersionId() {
        return packageVersionId;
    }

    /**
     * 创建流程包
     */
    public void testCreatePackage() {
        String apiBase = Constants.API_BASE;
        String companyId = Constants.COMPANY_ID;
        String departmentId = Constants.DEPARTMENT_ID;

        /** 获取用户Token */
        String token = RPAConsoleHelper.getToken();
        Assert.hasLength(token, "获取用户Token-成功");

        Map<String, String> headers = new HashMap();
        headers.put("Authorization", "Bearer " + token);
        headers.put("Content-type", "application/json");
        headers.put("DataEncoding", "UTF-8");
        headers.put("CompanyId", companyId);
        headers.put("DepartmentId", departmentId);

        /** 1.生成上传通道 */
        CreateUploadChannelResponseDto channelResponseDto = createUploadChannel(apiBase, headers);
        String channelId = channelResponseDto.getId();
        Assert.hasLength(channelId, "生成上传通道-成功");

        /** 2.上传流程包文件 */
        Channel channel = channelResponseDto.getChannel();
        Map<String, String> uploadFileHeaders = new HashMap();
        channel.getHeaders().keySet().forEach(x->{uploadFileHeaders.put(x, channel.getHeaders().get(x));} );
        String uploadUrl = channel.getUri();
        uploadPkgFile(uploadUrl, uploadFileHeaders);

        /** 3.标记流程包上传完成 */
        MarkUploadCompletedResponseDto uploadCompleteResponseDto = markUploadCompleted(apiBase, headers, channelId);

        // 设置packageId,packageVersionId给其它示例使用
        this.packageId = uploadCompleteResponseDto.getPackageId();
        this.packageVersionId = uploadCompleteResponseDto.getPackageVersionId();
        Assert.hasLength(this.packageId, "标记流程包上传完成-成功");
    }

    /**
     * API调用-（网页）生成上传通道
     */
    private CreateUploadChannelResponseDto createUploadChannel(String apiBase, Map<String, String> headers) {
        logger.info("【生成上传通道】开始");
        String resText = HttpRequestUtil.doPost(
                apiBase + "/openapi/preloadedversionfiles",
                headers,
                null);
        CreateUploadChannelResponseDto dto = (CreateUploadChannelResponseDto) JsonUtil.jsonToObject(resText, CreateUploadChannelResponseDto.class);
        logger.info("【生成上传通道】结束");
        return dto;
    }

    /**
     * API调用-通用上传文件流地址
     */
    private void uploadPkgFile(String uploadUrl, Map<String, String> headers) {
        logger.info("【上传流程包文件】开始");
        String dir = this.getClass().getClassLoader().getResource("").getPath();
        String filePath = dir + "EmptyPackage.dgs";
        String resText = HttpRequestUtil.doPut(
                uploadUrl,
                headers,
                new File(filePath));
        logger.info("【上传流程包文件】结束");
    }

    /**
     * API调用-（网页）通知上传完成
     */
    private MarkUploadCompletedResponseDto markUploadCompleted(String apiBase, Map<String, String> headers, String channelId) {
        logger.info("【标记流程包版本上传完成】开始");
        String resText = HttpRequestUtil.doPatch(
                apiBase + String.format("/openapi/preloadedversionfiles/%s", channelId),
                headers,null);
        MarkUploadCompletedResponseDto dto = (MarkUploadCompletedResponseDto) JsonUtil.jsonToObject(resText, MarkUploadCompletedResponseDto.class);
        logger.info("【标记流程包版本上传完成】结束");
        return dto;
    }
}
