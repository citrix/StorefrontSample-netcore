using Microsoft.AspNetCore.Mvc;

public class ApplicationsController : Controller
{
    [Route("Applications")]
    public IActionResult Index()
    {
        return View();
    }
}