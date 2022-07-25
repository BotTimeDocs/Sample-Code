using System;
using System.Collections.Generic;

namespace RPA.Abstractions
{
    public class PackageDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 下载计数
        public long TotalDownloads { get; set; }

        // 最高版本号
        public string LastVersion { get; set; }

        // 最高版本id
        public Guid? LastVersionId { get; set; }

        // 创建时间
        public DateTimeOffset CreatedAt { get; set; }

        // 更新时间
        public DateTimeOffset? ModifiedAt { get; set; }

        // 创建人id
        public Guid CreatedBy { get; set; }

        // 创建人名称
        public string CreatedByName { get; set; }

        // 更新人id
        public Guid? ModifiedBy { get; set; }

        // 更新人名称
        public string ModifiedByName { get; set; }

        // 标记（列表）
        public IList<string> Tags { get; set; }

        // 公司id
        public Guid CompanyId { get; set; }

        // 部门id
        public Guid DepartmentId { get; set; }

        // 图标存放地址
        public string IconUrl { get; set; }

        // 备注详情
        public string FullDescription { get; set; }

        // 流程包所有版本
        public List<PackageVersionDTO> Versions { get; set; }
    }
}