using OpenApi.Sdk.Utils;
using RPA.Abstractions;

namespace OpenApi.Samples.Ops
{
    public partial class ConsoleOps
    {
        // 查询机器人列表
        public PagedListResponseDTO<RobotDTO> QueryRobots(PagedListRequestDTO pagedListRequest)
        {
            var url = $"{API_BASE}/openapi/robots?" + BuildQueryString(pagedListRequest);
            return HttpUtility.Get<PagedListResponseDTO<RobotDTO>>(url, GetHeaders());
        }
    }
}