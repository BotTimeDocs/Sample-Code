using System;

namespace RPA.Abstractions
{
    public class RobotDTO
    {
        // 主键
        public Guid Id { get; set; }

        // 当前所在设备的id
        public Guid? CurrentDeviceId { get; set; }

        // 最近的心跳时间
        public DateTimeOffset? LastHeartbeatTime { get; set; }

        // 公司id
        public Guid CompanyId { get; set; }

        // 部门id
        public Guid DepartmentId { get; set; }

        // 机器人名称        
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 是否可升级
        public bool CanUpgrade { get; set; }

        // 机器人许可类型
        public string ClientSku { get; set; }

        // 机器人版本
        public string Version { get; set; }
    }
}