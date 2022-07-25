package com.encoo.demo.utils;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;

public class JsonUtil {
    public static Object jsonToObject(String resText, Class c) {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(resText, c);
        } catch (IOException e) {
            e.printStackTrace();
        }

        return  null;
    }

    public static String toJsonString(Object obj) {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.writeValueAsString(obj);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        return null;
    }
}
