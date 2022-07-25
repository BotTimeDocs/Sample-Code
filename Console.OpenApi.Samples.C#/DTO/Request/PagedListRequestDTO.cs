namespace RPA.Abstractions
{
    // 查询请求基础类，分页
    public class PagedListRequestDTO
    {
        // 页码，从0开始
        public virtual int PageIndex { get; set; } = 0;

        // 每页记录数
        public virtual int PageSize { get; set; } = 20;
    }
}