using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller {

    [HttpGet("")]
    public IActionResult Index() {
        return Content("QnA API Running.");
    }
}