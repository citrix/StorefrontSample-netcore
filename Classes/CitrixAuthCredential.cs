using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class CitrixAuthCredential
{
    public string AuthToken { get; set; }
    public string SessionID { get; set; }
    public string CSRFToken { get; set; }
    public string CookiePath { get; set; }
    public string CookieHost { get; set; }
    public string StorefrontUrl { get; set; }
    public CitrixAuthCredential()
    {
        
    }
}
