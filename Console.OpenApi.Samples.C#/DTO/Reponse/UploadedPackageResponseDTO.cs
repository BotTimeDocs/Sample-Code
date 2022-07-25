namespace RPA.Abstractions
{
    public class UploadedPackageResponseDTO
    {
        // 流程包id
        public string PackageId { get; set; }

        // 流程包版本id
        public string PackageVersionId { get; set; }

        // 流程包名称
        public string PackageName { get; set; }

        // 最新版本
        public string LastVersion { get; set; }
    }
}