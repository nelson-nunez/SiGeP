using System.Web;


namespace SiGeP.API.Common.Model
{
    public class WebApiConfig
    {
        public string BaseAddress { get; set; }

        public string Token { get; set; }

        public int Timeout { get; set; }
    }
}