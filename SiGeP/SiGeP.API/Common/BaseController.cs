using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiGeP.API.Common
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IJwtAuthManager jwtAuthManager;


        public BaseController(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }
    }
}

