namespace OpenApi.Samples
{
    public class ConstantsForTest
    {
        // 登录接口地址，需将10.10.30.43替换为自己服务器的IP,端口默认为81
        public static string SSO_URL = "http://10.10.30.43:81/connect/token";
        public static string USER_NAME = "admin@encootech.com";
        public static string USER_PWD = "123456";

        // API接口地址，需将10.10.30.43替换为自己服务器的IP,端口默认为8080
        public const string API_BASE = "http://10.10.30.43:8080";

        // 可从网页接口调用中获取，CompanyId、DepartmentId对于同一用户是固定的
        public static string COMPANY_ID = "9a0c7d89-404a-4896-a0b9-1add6d9551e8";
        public static string DEPARTMENT_ID = "2836f935-3c0b-4ce3-8cc3-77324f8df68c";
    }
}