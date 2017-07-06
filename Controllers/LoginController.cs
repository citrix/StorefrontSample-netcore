using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    [Route("Login")]
    public IActionResult Index()
    {
        return View();
    }
}