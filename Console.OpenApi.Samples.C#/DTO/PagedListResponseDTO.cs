using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 分页查询响应Dto
    public class PagedListResponseDTO<TDTO>
    {
        // （符合条件的）记录总数目
        public int Count { get; set; }

        // 当前页（查询条件指定）的所有记录
        public IEnumerable<TDTO> List { get; set; }
    }
}