using OpenApi.Samples;
using OpenApi.Samples.Ops;
using RPA.Abstractions;
using RPA.Abstractions.RPA.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OpenAPI.Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ops = new ConsoleOps(ConstantsForTest.SSO_URL, ConstantsForTest.API_BASE);
            var token = System.Threading.CancellationToken.None;
            // 登录
            ops.Login(ConstantsForTest.USER_NAME, ConstantsForTest.USER_PWD);
            // 设置公司、部门
            ops.CompanyId = ConstantsForTest.COMPANY_ID;
            ops.DepartmentId = ConstantsForTest.DEPARTMENT_ID;

            UploadedPackageResponseDTO uploadedPackage = Senario1(ops, token);
            Senario2(ops, Guid.Parse(uploadedPackage.PackageId), Guid.Parse(uploadedPackage.PackageVersionId));
            Senario3(ops, Guid.Parse(uploadedPackage.PackageId), Guid.Parse(uploadedPackage.PackageVersionId));

            // 删除流程包
            ops.DeletePackageById(Guid.Parse(uploadedPackage.PackageId));
        }

        //场景一：创建流程包、查询
        private static UploadedPackageResponseDTO Senario1(ConsoleOps ops, System.Threading.CancellationToken token)
        {
            Console.WriteLine("************************************************************************" +
                "\r\n场景一：创建流程包、查询");
            var filePath = @"test-dgs\\EmptyPackage.dgs";
            // 创建流程包方式1：直接上传流程包（dgs限200M）
            var uploadedPackage = ops.UploadPackageAsync(filePath);
            Console.WriteLine($"成功上传流程包版本{uploadedPackage.LastVersion},流程包名称:{uploadedPackage.PackageName}");

            // 创建流程包方式2：分步上传流程包（支持大文件）
            var channelDto = ops.CreateUploadChannel();
            ops.UploadDgsFile(channelDto.Channel.Uri, channelDto.Channel.Headers, filePath);
            uploadedPackage = ops.MarkPackageUploaded(channelDto.Id);
            Console.WriteLine($"成功上传流程包版本{uploadedPackage.LastVersion},流程包名称:{uploadedPackage.PackageName}");

            // 查询流程包
            var package = ops.GetPackageById(Guid.Parse(uploadedPackage.PackageId));
            Console.WriteLine($"流程包版本数量:{package.Versions.Count()} ");

            // 查询流程包
            var packages = ops.QueryPackages(new PagedListRequestDTO() { });
            Console.WriteLine($"返回流程包数量:{packages.List.Count()} ");
            return uploadedPackage;
        }

        //场景二：创建流程部署，使用机器人组作为执行目标;并查询、执行
        private static void Senario2(ConsoleOps ops, Guid packageId, Guid packageVersionId)
        {
            Console.WriteLine("************************************************************************" +
                "\r\n场景二：创建流程部署，使用机器人组作为执行目标;并查询、执行");
            var createWorkflowDto = new CreateWorkflowRequestDTO();
            createWorkflowDto.Name = "Workflow_Test_" + DateTime.Now.Ticks;

            var queues = ops.QueryQueues(new PagedListRequestDTO());
            if (queues.Count < 1)
            {
                throw new Exception("当前部门下未找到机器人组");
            }

            var queueId = queues.List.First().Id;
            createWorkflowDto.PackageId = packageId;
            createWorkflowDto.PackageVersionId = packageVersionId;
            createWorkflowDto.AssociatedQueueId = queueId;
            var workflow = ops.CreateWorkflow(createWorkflowDto);
            Console.WriteLine($"成功创建流程部署:{workflow.Name}");

            // 查询流程部署
            var workflows = ops.QueryWorkflows(new PagedListRequestDTO()).List;
            Console.WriteLine($"找到流程部署个数:{workflows.Count()}");

            // 更新流程部署
            var updatedDto = new UpdateWorkflowRequestDTO();
            updatedDto.Name = "Updated_" + workflow.Name;
            var updateWorkflow = ops.UpdateWorkflow(workflow.Id, updatedDto);
            Console.WriteLine($"流程部署已更名为:{updateWorkflow.Name}");

            // 更新流程部署到最新的流程包版本
            BatchUpgradeWorkflowRequestDTO requestDTO = new BatchUpgradeWorkflowRequestDTO();
            requestDTO.WorkflowIds = new List<Guid>() { workflow.Id };
            var upgradeResponse = ops.BatchUpgradeWorkflows(requestDTO);
            Console.WriteLine($"成功升级流程部署个数:{upgradeResponse.SuccessCount}");

            // 执行流程部署
            List<JobDTO> jobs = ops.ExecuteWorkflow(
                workflow.Id,
                new ExectueWorkflowRequestDTO()
                {
                    JobSubjectId = workflow.Id,
                    JobSubjectName = workflow.Name,
                    JobSubjectType = "Workflow"
                });
            Console.WriteLine($"成功执行流程部署:{updateWorkflow.Name},创建job数量:{jobs.Count}");

            // 删除流程部署
            ops.DeleteWorkflow(workflow.Id);
            Console.WriteLine($"成功删除流程部署:{updateWorkflow.Name}");
        }

        //场景三：创建流程部署，使用多个机器人作为执行目标
        private static CreateWorkflowRequestDTO Senario3(ConsoleOps ops, Guid packageId, Guid packageVersionId)
        {
            Console.WriteLine("************************************************************************" +
                "\r\n场景三：创建流程部署，使用多个机器人作为执行目标");

            CreateWorkflowRequestDTO createWorkflowDto = new CreateWorkflowRequestDTO();
            createWorkflowDto.Name = "Workflow_with-robots-Test_" + DateTime.Now.Ticks;
            createWorkflowDto.PackageId = packageId;
            createWorkflowDto.PackageVersionId = packageVersionId;
            var workflowWithRobots = ops.CreateWorkflow(createWorkflowDto);
            Console.WriteLine($"成功创建流程部署:{workflowWithRobots.Name}");

            var robots = ops.QueryRobots(new PagedListRequestDTO());
            ops.BindRobotsToWorkflow(
                workflowWithRobots.Id,
                new BatchBindRobotToWorkflowRequestDTO() { RobotIds = robots.List.Take(2).Select(x => x.Id).ToList() });
            var bindedRobots = ops.QueryWorkflowBindedRobots(workflowWithRobots.Id).List;
            Console.WriteLine($"流程部署目标执行机器人数:{bindedRobots.Count()}");

            ops.UnBindRobotToWorkflow(workflowWithRobots.Id, bindedRobots.First().Id);
            bindedRobots = ops.QueryWorkflowBindedRobots(workflowWithRobots.Id).List;
            Console.WriteLine($"移除后，流程部署目标执行机器人数:{bindedRobots.Count()}");

            // 删除流程部署
            ops.DeleteWorkflow(workflowWithRobots.Id);
            Console.WriteLine($"成功删除流程部署:{workflowWithRobots.Name}");
            return createWorkflowDto;
        }
    }
}