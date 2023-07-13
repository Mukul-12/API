using System.Net;

namespace API.Repository
{
    public class ApiResponses
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public Object Result { get; set; }

    }
}
