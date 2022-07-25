namespace RPA.Abstractions
{
    // 资产实体
    public class AssetContentEntity
    {
        // 名称
        public string Name { get; set; }

        // 备注
        public string Description { get; set; }

        // 是否加密
        public bool IsRequiredEncryption { get; set; }
    }
}