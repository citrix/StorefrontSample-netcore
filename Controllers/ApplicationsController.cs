using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text;

public class ApplicationsController : Controller
{
    public IActionResult Index()
    {

        if ( TempData.ContainsKey("applist") )
        {
            HttpContext.Session.SetString("applist",TempData["applist"].ToString());

            var applist = JsonConvert.DeserializeObject<List<CitrixApplicationInfo>>(TempData["applist"].ToString());

            return View(applist);
        }
        else
        {
            return View(new List<CitrixApplicationInfo>());
        }
        
    }

    public async Task<IActionResult> Launch(string AppID)
    {
        var applist = JsonConvert.DeserializeObject<List<CitrixApplicationInfo>>(HttpContext.Session.GetString("applist"));

        var application = applist.SingleOrDefault(appInfo => appInfo.ID == AppID);

        string icaFile = null;

        if ( application != null )
        {
            icaFile = await Storefront.RetreiveICA(application.Auth.StorefrontUrl,application.Auth,application);
        }
        ASCIIEncoding  encoding = new ASCIIEncoding();
        byte[] bytes = Encoding.ASCII.GetBytes(icaFile);
        
        return File(bytes,"application/x-ica");
        
    }
   
}