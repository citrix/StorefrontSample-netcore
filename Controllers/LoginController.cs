using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Process(LoginInfo loginInfo)
    {
        // Store the server address and weburl in the session for further requests. Not the best
        // implementation, but the sample is about displaying applications. Another sanple should
        // show best practices about authentication.
        HttpContext.Session.SetString("SFAddress",loginInfo.SFAddress);
        HttpContext.Session.SetString("SFWebURL",loginInfo.SFWebURL);

        //process API
        var auth = await Storefront.AuthenticateWithPost(loginInfo.SFAddress,loginInfo.SFWebURL,loginInfo.Username,loginInfo.Password,loginInfo.Domain,loginInfo.UseSSL);
        //get resources
        var resources = await Storefront.GetResources(loginInfo.SFAddress,loginInfo.SFWebURL,auth,false);
    
        TempData["applist"] = JsonConvert.SerializeObject(resources);

        //redirect to the application listing page.
        return RedirectToAction("Index","Applications");
    }
}