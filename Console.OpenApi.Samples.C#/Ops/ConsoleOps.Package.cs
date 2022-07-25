using OpenApi.Sdk.Utils;
using RPA.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenApi.Samples.Ops
{
    public partial class ConsoleOps
    {
        // 查询流程包
        public PagedListResponseDTO<PackageDTO> QueryPackages(PagedListRequestDTO pagedListRequest)
        {
            var url = $"{API_BASE}/openapi/packages?IncludeVersions=true&" + BuildQueryString(pagedListRequest);
            return HttpUtility.Get<PagedListResponseDTO<PackageDTO>>(url, GetHeaders());
        }

        // 根据id获取流程包
        public PackageDTO GetPackageById(Guid id)
        {
            var url = $"{API_BASE}/openapi/packages/{id}";
            return HttpUtility.Get<PackageDTO>(url, GetHeaders());
        }

        // 根据id删除流程包
        public void DeletePackageById(Guid id)
        {
            var url = $"{API_BASE}/openapi/packages/{id}";
            HttpUtility.Delete(url, GetHeaders());
        }

        // 创建上传通道
        public CreateUploadChannelResponseDTO CreateUploadChannel()
        {
            var url = $"{API_BASE}/openapi/preloadedVersionFiles";
            return HttpUtility.Post<CreateUploadChannelResponseDTO>(url, GetHeaders());
        }

        // 上传dgs文件
        public void UploadDgsFile(string uploadUrl, Dictionary<string, string> headers, string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open);
            HttpUtility.Put(uploadUrl, headers, stream);
        }

        // 上传dgs文件并创建流程包
        public UploadedPackageResponseDTO UploadPackageAsync(string filePath)
        {
            Dictionary<string, string> headers = GetHeaders(addContentType_ApplicationJson: false);
            headers["Content-type"] = "application/octet-stream";
            var url = $"{API_BASE}/openapi/packages";
            using var stream = new FileStream(filePath, FileMode.Open);
            return HttpUtility.Put<UploadedPackageResponseDTO>(url, headers, stream);
        }

        // 通知dgs文件上传完成
        public UploadedPackageResponseDTO MarkPackageUploaded(string tempId)
        {
            var url = $"{API_BASE}/openapi/preloadedVersionFiles/{tempId}";
            return HttpUtility.Patch<UploadedPackageResponseDTO>(url, GetHeaders());
        }
    }
}