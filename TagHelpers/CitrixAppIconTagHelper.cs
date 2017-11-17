using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

public class CitrixAppIconTagHelper : TagHelper
{
    public CitrixApplicationInfo AppItem { get; set; }

    public int ImageSize { get; set; }

    public string AppID { get; set; }

   public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
   {
        try
        {
            output.TagName = "img";

            output.Attributes.Add("app-id",AppID);

            if ( ImageSize != 0 )
            {
                output.Attributes.Add("height",ImageSize);
                output.Attributes.Add("width",ImageSize);
            }

            var image = await Storefront.GetImage(AppItem.Auth.StorefrontUrl,AppItem);

            if ( image != null )
            {
                var b64Image = Storefront.GetBase64(image);
                output.Attributes.Add("src",$"data:image/jpeg;base64,{b64Image}");
            }
            else
            {
                //load default 
                output.Attributes.Add("src","http://via.placeholder.com/150x150");
            }
            
            
        }
        catch ( Exception Err)
        {
            Debug.WriteLine(Err.InnerException);
        }
       
       //output.Attributes.Add("src","testing.jpg");
   }
}