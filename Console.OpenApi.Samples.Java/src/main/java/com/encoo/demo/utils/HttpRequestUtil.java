package com.encoo.demo.utils;

import com.encoo.demo.Main;
import org.apache.http.HttpEntity;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.client.methods.*;
import org.apache.http.entity.FileEntity;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import org.apache.log4j.Logger;

import java.io.File;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.Map;

public class HttpRequestUtil {
    private static Logger logger = Logger.getLogger(Main.class);

    public static String doGet(String url, Map<String, String> headers) {
        HttpGet httpGet = new HttpGet(url);
        return Execute(httpGet, headers);
    }

    public static String doPatch(String url, Map<String, String> headers, String data) {
        HttpPatch httpPatch = new HttpPatch(url);
        if (data != null) {
            try {
                httpPatch.setEntity(new StringEntity(data));
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
                throw new RuntimeException(e);
            }
        }
        return Execute(httpPatch, headers);
    }

    public static String doPost(String url, Map<String, String> headers, String data) {
        HttpPost httpPost = new HttpPost(url);
        if (data != null) {
            try {
                httpPost.setEntity(new StringEntity(data));
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
                throw new RuntimeException(e);
            }
        }
        return Execute(httpPost, headers);
    }

    public static String doPut(String url, Map<String, String> headers, String data) {
        HttpPut httpPut = new HttpPut(url);
        try {
            httpPut.setEntity(new StringEntity(data));
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
            throw new RuntimeException(e);
        }
        return Execute(httpPut, headers);
    }

    public static String doPut(String url, Map<String, String> headers, File file) {
        HttpPut httpPut = new HttpPut(url);
        FileEntity fileEntity = new FileEntity(file);
        httpPut.setEntity(fileEntity);
        return Execute(httpPut, headers);
    }

    public static String Execute(
            HttpRequestBase requestBase,
            Map<String, String> headers) {
        CloseableHttpClient httpClient = HttpClients.createDefault();
        RequestConfig requestConfig = RequestConfig.custom()
                .setConnectTimeout(35000)
                .setConnectionRequestTimeout(35000)
                .setSocketTimeout(60000).build();
        requestBase.setConfig(requestConfig);
        headers.forEach((k, v) -> {
            requestBase.setHeader(k, v);
        });
        CloseableHttpResponse httpResponse = null;
        try {
            httpResponse = httpClient.execute(requestBase);
            HttpEntity entity = httpResponse.getEntity();
            String result = null;
            if (entity != null) {
                result = EntityUtils.toString(entity);
            }

            int statusCode = httpResponse.getStatusLine().getStatusCode();
            logger.info("Response status code:" + statusCode);
            logger.info("Response text:" + result);
            if (statusCode < 200 || statusCode >= 300) {
                throw new RuntimeException("请求失败，status code:" + statusCode);
            }
            return result;
        } catch (ClientProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (httpResponse != null) {
                try {
                    httpResponse.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            if (null != httpClient) {
                try {
                    httpClient.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

        return null;
    }
}