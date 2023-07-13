using Microsoft.AspNetCore.Mvc;
using static Utility.SD;

namespace Web.Models
{
    public class ApiRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;

        public string Url { get; set; }
        public object Data { get; set; }
    }
}
