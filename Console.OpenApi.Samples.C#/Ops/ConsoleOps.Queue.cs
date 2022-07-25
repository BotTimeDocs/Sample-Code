using OpenApi.Sdk.Utils;
using RPA.Abstractions;

namespace OpenApi.Samples.Ops
{
    public partial class ConsoleOps
    {
        // 查询机器人组列表
        public PagedListResponseDTO<QueueDTO> QueryQueues(PagedListRequestDTO pagedListRequest)
        {
            var url = $"{API_BASE}/openapi/queues?" + BuildQueryString(pagedListRequest);
            return HttpUtility.Get<PagedListResponseDTO<QueueDTO>>(url, GetHeaders());
        }
    }
}