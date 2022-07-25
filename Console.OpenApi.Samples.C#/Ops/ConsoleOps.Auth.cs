using OpenApi.Sdk.Utils;
using RPA.Abstractions;
using System.Collections.Generic;

namespace OpenApi.Samples.Ops
{
    public partial class ConsoleOps
    {
        public string UserName { get; set; }

        public string CompanyId { get; set; }

        public string DepartmentId { get; set; }

        private string token;

        private string API_BASE { get; set; }
        private string SSO_URL { get; set; }

        public ConsoleOps(string ssoUrl, string apiBaseUrl)
        {
            SSO_URL = ssoUrl;
            API_BASE = apiBaseUrl;
        }

        // 登录
        public string Login(string userName, string password)
        {
            string args = $@"client_id=thirdpartyservice&grant_type=password&username={userName}&password={password}";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-type", "application/x-www-form-urlencoded");
            var dto = HttpUtility.Post<GetTokenResponseDTO>(SSO_URL, headers, args);
            if (string.IsNullOrWhiteSpace(dto?.Access_token))
            {
                return dto?.Access_token;
            }

            this.UserName = userName;
            this.token = dto.Access_token;
            return dto.Access_token;
        }

        private Dictionary<string, string> GetHeaders(bool addContentType_ApplicationJson = true)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            if (addContentType_ApplicationJson) headers.Add("Content-type", "application/json");
            headers.Add("CompanyId", CompanyId);
            headers.Add("DepartmentId", DepartmentId);
            headers.Add("Authorization", $"Bearer {this.token}");
            return headers;
        }

        private string BuildQueryString(PagedListRequestDTO pagedListRequest)
        {
            return $"PageIndex={pagedListRequest.PageIndex}&&PageSize={pagedListRequest.PageSize}";
        }
    }
}