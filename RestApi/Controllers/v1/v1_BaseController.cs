using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers.v1;
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class v1_BaseController : ControllerBase
{
}
