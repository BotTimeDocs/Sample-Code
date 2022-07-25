namespace RPA.Abstractions
{
    // 创建上传通过响应dto
    public class CreateUploadChannelResponseDTO
    {
        // 主键id
        public string Id { get; set; }

        // 上传通道
        public Channel Channel { get; set; }
    }
}