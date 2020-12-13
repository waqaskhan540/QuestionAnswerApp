using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{

    [HttpGet("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        return Content("QnA API Running.");
    }
}
