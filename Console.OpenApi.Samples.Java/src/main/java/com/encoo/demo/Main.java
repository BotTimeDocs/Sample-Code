package com.encoo.demo;

import ch.qos.logback.classic.Level;
import ch.qos.logback.classic.LoggerContext;
import com.encoo.demo.sample.PackageSample;
import com.encoo.demo.sample.WorkflowSample;
import org.slf4j.LoggerFactory;

import java.util.List;

public class Main {
    static {
        LoggerContext loggerContext = (LoggerContext) LoggerFactory.getILoggerFactory();
        List<ch.qos.logback.classic.Logger> loggerList = loggerContext.getLoggerList();
        for (int i = 0; i < loggerList.size(); i++) {
            // 关闭Debug日志
            loggerList.get(i).setLevel(Level.INFO);
        }
    }

    public static void main(String args[]) {
        PackageSample packageTest = new PackageSample();
        packageTest.testCreatePackage();

        WorkflowSample workflowTest = new WorkflowSample(
                packageTest.getPackageId(),
                packageTest.getPackageVersionId(),
                Constants.Queue_ID
        );

        workflowTest.testCreateWorkflowAndExecution();
    }
}

