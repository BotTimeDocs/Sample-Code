using RPA.Abstractions.Enum;

namespace RPA.Abstractions
{
    public class PackageVersionArgument
    {
        // 参数名称
        public string Name { get; set; }

        // 参数类型
        public string Type { get; set; }

        // 参数方向
        public PackageVersionArgumentDirection Direction { get; set; }

        // 参数默认值
        public string DefaultValue { get; set; }

        // 使用的资产
        public AssetContentEntity AssetContent { get; set; }
    }
}